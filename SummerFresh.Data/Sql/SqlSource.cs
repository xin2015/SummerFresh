using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;
using System.Linq;
using SummerFresh.Basic;
using SummerFresh.Util;

namespace SummerFresh.Data.Sql
{
    public class SqlSource : IDisposable, ISqlSource
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        private const string CONFIG_FILTER = "*.config";
        private const string SQL_DIRECTORY_CONFIG_KEY = "SummerFresh.Data.SqlCommandsDirectory";
        private const string SQL_DIRECTORY_NAME = "SqlCommands";

        private static Regex GlobalResourceNamePattern = new Regex(@"^.+\.g(\..+\.|\.)resources$");
        private static Regex SqlResourceNamePattern = new Regex(@"^.+(\.|/)SqlCommands(\..+|\.|/.+|/)[^\./]+\.config$", RegexOptions.IgnoreCase);

        private Dictionary<Assembly, bool> _loadedAssemblies = new Dictionary<Assembly, bool>();
        private Dictionary<string, ISqlStatement> _sqlsMap;
        private Dictionary<string, ISqlStatement> _sqlsMapInDb;
        private FileSystemWatcher _configWatcher;
        private string _sqlsDir;
        private readonly ReaderWriterLockSlim _sqlsMapLock = new ReaderWriterLockSlim();
        private readonly ReaderWriterLockSlim _loadeAssembliesLock = new ReaderWriterLockSlim();
        private readonly ReaderWriterLockSlim _sqlsMapInDbLock = new ReaderWriterLockSlim();
        private bool _loading = false;
        private bool _disposed = false;
        private readonly object _realoadSyncRoot = new object();
        private readonly object _disposeSyncRoot = new object();

        public SqlSource()
        {
            _sqlsMap = new Dictionary<string, ISqlStatement>(StringComparer.OrdinalIgnoreCase);
            _sqlsMapInDb = new Dictionary<string, ISqlStatement>(StringComparer.OrdinalIgnoreCase);
        }

        public Dictionary<string,ISqlStatement> Sqls
        {
            get
            {
                return _sqlsMap;
            }
        }

        public string SqlsDirectory
        {
            get { return _sqlsDir; }
        }

        public bool IsValidKey(string key)
        {
            return null != key && key.Trim().IndexOf(' ') <= 0 && key.IndexOf(',') < 0 && key.IndexOf('(') < 0;
        }

        public bool Add(string key, ISqlStatement statement, bool overwrite = true)
        {
            if (overwrite)
            {
                _sqlsMapLock.EnterWriteLock();
                try
                {
                    _sqlsMap[key] = statement;
                    return true;
                }
                finally
                {
                    _sqlsMapLock.ExitWriteLock();
                }

            }
            else
            {
                _sqlsMapLock.EnterUpgradeableReadLock();
                try
                {
                    if (_sqlsMap.ContainsKey(key))
                    {
                        return false;
                    }
                    else
                    {
                        _sqlsMapLock.EnterWriteLock();
                        try
                        {
                            _sqlsMap[key] = statement;
                            return true;
                        }
                        finally
                        {
                            _sqlsMapLock.ExitWriteLock();
                        }
                    }
                }
                finally
                {
                    _sqlsMapLock.ExitUpgradeableReadLock();
                }
            }
        }

        public SqlSource LoadSqls()
        {
            _sqlsDir = GetSqlsDirectory();

            if (null != _sqlsDir)
            {
                LoadSqlStatements();

                //监控文件变化
                _configWatcher = new FileSystemWatcher(_sqlsDir, CONFIG_FILTER)
                {
                    IncludeSubdirectories = true,
                    EnableRaisingEvents = true,
                };

                _configWatcher.Changed += new FileSystemEventHandler(OnChanged);
                _configWatcher.Created += new FileSystemEventHandler(OnChanged);
                _configWatcher.Deleted += new FileSystemEventHandler(OnChanged);
                _configWatcher.Renamed += new RenamedEventHandler(OnRenamed);
            }
            else
            {
                log.Warn("sql commands directory not found");
            }

            //监听后续动态加载的Assembly的加载
            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(AssemblyLoadHandler);

            return this;
        }

        private void AssemblyLoadHandler(Object sender, AssemblyLoadEventArgs args)
        {
            LoadSqlsFromAssembly(args.LoadedAssembly, false);
        }

        public ISqlStatement Find(string key, string daoProviderName)
        {
            ISqlStatement statement = null;
            if (!string.IsNullOrEmpty(daoProviderName))
            {
                statement = Find(key + "$" + daoProviderName.ToLower());
            }

            return statement ?? Find(key);
        }

        public ISqlStatement Find(string key)
        {
            ISqlStatement sql = null;
            _sqlsMapLock.EnterReadLock();
            try
            {
                _sqlsMap.TryGetValue(key, out sql);
            }
            finally
            {
                _sqlsMapLock.ExitReadLock();
            }
            if (sql == null)
            {
                sql = FindInDb(key);
            }
            return sql;
        }

        public ISqlStatement FindInDb(string key)
        {
            ISqlStatement sql = null;
            _sqlsMapInDbLock.EnterReadLock();
            try
            {
                _sqlsMapInDb.TryGetValue(key, out sql);
            }
            finally
            {
                _sqlsMapInDbLock.ExitReadLock();
            }
            return sql;
        }

        protected void LoadSqlsFromAssemblies()
        {
            //TODO : 写死忽略的Assemblies
            string[] excludes =
                new string[]
                    {
                        "Microsoft", "System", "mscorlib", "log4net"
                    };
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!excludes.Any(name => assembly.GetName().Name.StartsWith(name)))
                {
                    LoadSqlsFromAssembly(assembly, false);
                }
            }
        }

        public void LoadSqlsFromAssembly(Assembly assembly, bool overrideSql)
        {
            Dictionary<string, ISqlStatement> sqls = LoadSqlsFromAssembly(assembly);

            //如果key不存在于全局的sql集合中，添加进去
            sqls.ForEach(pair =>
            {
                _sqlsMapLock.EnterUpgradeableReadLock();
                try
                {
                    if (!_sqlsMap.ContainsKey(pair.Key) || (_sqlsMap.ContainsKey(pair.Key) && overrideSql))
                    {
                        log.Info("load sql command '{0}' from assembly '{1}'", pair.Key, assembly.GetName().Name);
                        _sqlsMapLock.EnterWriteLock();
                        try
                        {
                            _sqlsMap.Add(pair.Key, pair.Value);
                        }
                        finally
                        {
                            _sqlsMapLock.ExitWriteLock();
                        }
                    }
                }
                finally
                {
                    _sqlsMapLock.ExitUpgradeableReadLock();
                }
            });
        }

        /*
        protected ISqlStatement FindFromCurrentAssembly(string key)
        {
            _loadeAssembliesLock.EnterUpgradeableReadLock();
            try
            {
                Assembly assembly = Assembly.GetCallingAssembly();
                if (!_loadedAssemblies.ContainsKey(assembly))
                {
                    Dictionary<string, ISqlStatement> sqls = LoadSqlsFromAssembly(assembly);

                    //如果key不存在于全局的sql集合中，添加进去
                    sqls.ForEach(pair =>
                        {
                            _sqlsMapLock.EnterUpgradeableReadLock();
                            try
                            {
                                if (!_sqlsMap.ContainsKey(pair.Key))
                                {
                                    log.Info("load sql command '{0}' from assembly '{1}'", pair.Key, assembly.GetName().Name);
                                    _sqlsMapLock.EnterWriteLock();
                                    try
                                    {
                                        _sqlsMap.Add(pair.Key, pair.Value);
                                    }
                                    finally
                                    {
                                        _sqlsMapLock.ExitWriteLock();
                                    }
                                }
                            }
                            finally
                            {
                                _sqlsMapLock.ExitUpgradeableReadLock();
                            }
                        });

                    _loadeAssembliesLock.EnterWriteLock();
                    try
                    {
                        _loadedAssemblies.Add(assembly, true);
                    }
                    finally
                    {
                        _loadeAssembliesLock.ExitWriteLock();
                    }

                    ISqlStatement sql;
                    sqls.TryGetValue(key, out sql);
                    sqls.Clear();
                    return sql;
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                _loadeAssembliesLock.ExitUpgradeableReadLock();
            }
        }
         */

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            log.Debug("File '{0}' Changed,Reload all commands", e.Name);
            LoadSqlStatements();
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            log.Debug("File '{0}' Renamed To '{1}',Reload all commands", e.OldName, e.Name);
            LoadSqlStatements();
        }

        private static Dictionary<string, ISqlStatement> LoadSqlsFromAssembly(Assembly assembly)
        {
            Dictionary<string, ISqlStatement> sqls = new Dictionary<string, ISqlStatement>();

            string[] resources = null;
            try
            {
                resources = assembly.GetManifestResourceNames();
            }
            catch (Exception e)
            {
                log.Debug("calling GetManifestResourceNames in assembly '{0}' error", assembly.GetName().Name, e);
                return sqls;
            }

            foreach (string resource in resources)
            {
                using (Stream stream = assembly.GetManifestResourceStream(resource))
                {
                    if (null != stream)
                    {
                        if (GlobalResourceNamePattern.IsMatch(resource))
                        {
                            using (ResourceReader reader = new ResourceReader(stream))
                            {
                                foreach (DictionaryEntry entry in reader)
                                {
                                    if (entry.Key is string && entry.Value is Stream)
                                    {
                                        string file = entry.Key as string;

                                        if (!SqlResourceNamePattern.IsMatch(file))
                                        {
                                            continue;
                                        }

                                        Dictionary<string, ISqlStatement> loadedSqls =
                                            LoadSqlsFromStream(file, entry.Value as Stream);

                                        log.Debug("load {0} sqls from '{1}' in assembly '{2}'",
                                                  loadedSqls.Count, file, assembly.GetName().Name);

                                        loadedSqls.ForEach(pair => sqls[pair.Key] = pair.Value);
                                    }
                                }
                            }
                        }
                        else if (SqlResourceNamePattern.IsMatch(resource))
                        {
                            Dictionary<string, ISqlStatement> loadedSqls = LoadSqlsFromStream(resource, stream);

                            log.Debug("load {0} sqls from '{1}' in assembly '{2}'",
                                      loadedSqls.Count, resource, assembly.GetName().Name);

                            loadedSqls.ForEach(pair => sqls[pair.Key] = pair.Value);
                        }
                    }
                }
            }

            return sqls;
        }

        private void LoadSqlStatements()
        {
            lock (_realoadSyncRoot)
            {
                if (_loading)
                {
                    log.Debug("config is loading...,exit");
                    return;
                }
                _loading = true;
            }

            string[] files = Directory.GetFiles(_sqlsDir, CONFIG_FILTER, SearchOption.AllDirectories);

            _sqlsMapLock.EnterWriteLock();
            try
            {
                _sqlsMap.Clear();
                foreach (string file in files)
                {
                    log.Debug("loading sql commands from  '{0}'", file);

                    Dictionary<string, ISqlStatement> sqls =
                        LoadSqlsFromStream(file, new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read));

                    sqls.ForEach(pair =>
                    {
                        if (_sqlsMap.ContainsKey(pair.Key))
                        {
                            throw new InvalidOperationException(string.Format("duplicate key '{0}' found in '{1}'", pair.Key, file));
                        }

                        _sqlsMap.Add(pair.Key, pair.Value);
                    });
                }
            }
            catch (Exception exception)
            {
                log.Error("loading sql files cause exception", exception);
                throw;
            }
            finally
            {
                _sqlsMapLock.ExitWriteLock();
                lock (_realoadSyncRoot)
                {
                    _loading = false;
                }
            }

            //加载资源文件中的Sql
            LoadSqlsFromAssemblies();
        }

        private static Dictionary<string, ISqlStatement> LoadSqlsFromStream(string file, Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var root = XElement.Load(reader);

                return LoadSqlsFromXml(file, root);
            }
        }

        private static Dictionary<string, ISqlStatement> LoadSqlsFromXml(string file, XElement root)
        {
            Dictionary<string, ISqlStatement> sqls = new Dictionary<string, ISqlStatement>();

            //支持配置文件编写如Users.Oracle.config来表示SQL只适用于某个数据库类型
            string subfix = "";
            string daoProviderName = ExtractDaoProviderName(file);
            if (!string.IsNullOrEmpty(daoProviderName) &&
                null != DaoFactory.GetDaoProvider(daoProviderName))
            {
                subfix = "$" + daoProviderName.ToLower();
            }

            foreach (XElement element in root.Elements())
            {
                XAttribute attribute = element.Attribute("key");

                if (null == attribute || string.IsNullOrEmpty(attribute.Value))
                {
                    throw new InvalidOperationException(string.Format("'key' attribute must has a value of command in '{0}'", file));
                }

                string key = attribute.Value + subfix;

                // default sql don't support @ or ： native db named param
                XAttribute ignoreDbNamedParam = element.Attribute("ignoreDbNamedParam");
                ISqlStatement statement = SqlParser.Parse(element.Value, null != ignoreDbNamedParam && ignoreDbNamedParam.Value.Equals("true", StringComparison.OrdinalIgnoreCase));

                XAttribute connection = element.Attribute("connection");
                if (null != connection)
                {
                    statement.Connection = connection.Value;
                }

                sqls[key] = statement;
            }

            return sqls;
        }

        public void Dispose()
        {
            lock (_disposeSyncRoot)
            {
                if (_disposed)
                {
                    return;
                }
            }

            if (null != _configWatcher)
            {
                _configWatcher.Dispose();
            }
        }

        ~SqlSource()
        {
            this.Dispose();
        }

        private static string GetSqlsDirectory()
        {
            DirectoryInfo dir;

            if (!(AppUtility.FindDirectoryOrConfig(SQL_DIRECTORY_CONFIG_KEY, SQL_DIRECTORY_NAME, out dir) ||
                 AppUtility.FindConfigDirectory(SQL_DIRECTORY_NAME, out dir)))
            {
                return null;
            }

            return dir.FullName;
        }

        private static string ExtractDaoProviderName(string file)
        {
            FileInfo info = new FileInfo(file);

            int index = info.Name.LastIndexOf(".");

            if (index > 0)
            {
                string name = info.Name.Substring(0, index);

                index = name.LastIndexOf(".");

                if (index > 0)
                {
                    return name.Substring(index + 1);
                }
            }

            return null;
        }
    }
}