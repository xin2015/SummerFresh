using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Transactions;
using SummerFresh.Data.Mapping;
using SummerFresh.Data.Provider;
using SummerFresh.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.SqlCe;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using SummerFresh.Util;
using SummerFresh.Basic;
using SummerFresh.Data.MySql;
namespace SummerFresh.Data
{
    public sealed class DaoFactory
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        public const string DefaultConnectionName = "DefaultDB";

        private static ISqlSource _sqlSource;
        private static IDictionary<string, Dao> _daos;
        private static IDictionary<string, IDaoProvider> _providers;
        private static IEnumerable<ISqlParameters> _parameters;
        private static IDictionary<string, IDaoProvider> _providerMapping;
        private static readonly ReaderWriterLockSlim _providerMappingLock = new ReaderWriterLockSlim();
        private static readonly ReaderWriterLockSlim _daosLock = new ReaderWriterLockSlim();
        private static NameObjectCollection<ISqlActionExecutor> _actionExecutors;
        private static LinkedList<IMappingProvider> _mappingProviders;

        static DaoFactory()
        {
            Initialize();
        }

        /// <summary>
        /// 从IOC容器中获取外部注入的sql参数解析器
        /// </summary>
        public static IEnumerable<ISqlParameters> Parameters
        {
            get { return _parameters; }
        }

        /// <summary>
        /// 获取连接串名为DefaultDB的数据库连接
        /// </summary>
        public static Dao GetDao()
        {
            return GetDao(DefaultConnectionName);
        }

        /// <summary>
        /// 获取指定连接串名字的数据库连接
        /// </summary>
        /// <param name="name">数据库连接串</param>
        /// <returns></returns>
        public static Dao GetDao(String name)
        {
            Dao dao;

            _daosLock.EnterUpgradeableReadLock();
            try
            {
                if (!_daos.TryGetValue(name, out dao))
                {
                    _daosLock.EnterWriteLock();
                    try
                    {
                        dao = new DatabaseDao(name, CreateDatabase(name));
                        _daos.Add(name, dao);
                    }
                    finally
                    {
                        _daosLock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                _daosLock.ExitUpgradeableReadLock();
            }

            return dao;
        }

        /// <summary>
        /// 获取指定数据库连接串和数据库提供者的数据库连接
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="providerName">数据库提供者</param>
        /// <returns>数据库连接，目前只支持Oracle、sql server</returns>
        public static Dao GetDao(String connectionString, String providerName)
        {
            Dao dao;

            _daosLock.EnterUpgradeableReadLock();
            try
            {
                if (!_daos.TryGetValue(connectionString, out dao))
                {
                    _daosLock.EnterWriteLock();
                    try
                    {
                        dao = new DatabaseDao(connectionString, CreateDatabase(connectionString, providerName), providerName);
                        _daos.Add(connectionString, dao);
                    }
                    finally
                    {
                        _daosLock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                _daosLock.ExitUpgradeableReadLock();
            }

            return dao;
        }

        /// <summary>
        /// 获取连接串为DefaultDB的企业库Database对象
        /// </summary>
        /// <returns>企业库Database对象</returns>
        public static Database GetDatabase()
        {
            return GetDatabase(DefaultConnectionName);
        }

        /// <summary>
        /// 获取指定名称的企业库Database对象
        /// </summary>
        /// <param name="name">连接串名</param>
        /// <returns>企业库Database对象</returns>
        public static Database GetDatabase(string name)
        {
            return GetDao(name).Database;
        }

        /// <summary>
        /// 从IOC容器中获取sql来源对象，便于扩展自己的数据来源
        /// </summary>
        /// <returns>数据来源</returns>
        public static ISqlSource GetSqlSource()
        {
            return _sqlSource;
        }

        /// <summary>
        /// 从IOC容器中获取action参数解析器
        /// </summary>
        public static NameObjectCollection<ISqlActionExecutor> ActionExecutors
        {
            get { return _actionExecutors; }
        }

        /// <summary>
        /// 获取指定名字的数据库方言提供者
        /// </summary>
        /// <param name="providerName">提供者名字</param>
        internal static IDaoProvider GetDaoProvider(string providerName)
        {
            return _providers.Values.SingleOrDefault(p => p.Name.Equals(providerName,StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 轮询所有的数据库方言提供者，并返回制定名称的方言提供者
        /// </summary>
        /// <param name="dbProviderName">方言提供者名称</param>
        internal static IDaoProvider GetDaoProviderOfDbProvider(string dbProviderName)
        {
            IDaoProvider provider;

            _providerMappingLock.EnterUpgradeableReadLock();
            try
            {
                if (!_providerMapping.TryGetValue(dbProviderName, out provider))
                {
                    provider = _providers.Values.SingleOrDefault(p => p.SupportsDbProvider(dbProviderName));

                    if (null != provider)
                    {
                        _providerMappingLock.EnterWriteLock();
                        try
                        {
                            _providerMapping.Add(dbProviderName, provider);
                        }
                        finally
                        {
                            _providerMappingLock.ExitWriteLock();
                        }
                    }
                }
            }
            finally
            {
                _providerMappingLock.ExitUpgradeableReadLock();
            }

            return provider;
        }

        /// <summary>
        /// 根据DbProvider的名称返回对应的IMappingProvider
        /// </summary>
        /// <param name="dbProviderName">连接串配置中的provider name</param>
        public static IMappingProvider GetMappingProvider(string dbProviderName)
        {
            return _mappingProviders.Single(r => r.Supports(dbProviderName));
        }

        /// <summary>
        /// 根据连接串名字创建企业库Database对象
        /// </summary>
        /// <param name="name">连接串参数</param>
        /// <returns></returns>
        private static Database CreateDatabase(string name)
        {
            try
            {                
                return DatabaseFactory.CreateDatabase(name);
            }
            catch (ResolutionFailedException e)
            {
                throw new DaoException(string.Format("Please check is the connection string '{0}' exists and correct", name),
                                       e);
            }
            catch (ActivationException e)
            {
                throw new DaoException(string.Format("Please check is the connection string '{0}' exists and correct", name),
                                       e);
            }
        }

        /// <summary>
        /// 根据连接串和数据库提供者，创建企业库Database对象
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="providerName">数据库提供者</param>
        /// <returns>企业库Database对象</returns>
        private static Database CreateDatabase(string connectionString, string providerName)
        {
            if (String.IsNullOrEmpty(connectionString) || String.IsNullOrEmpty(providerName))
            {
                throw new DaoException("When CreateDatabase, connectionString and providerName must pass in");
            }

            if ("System.Data.SqlClient".Equals(providerName.Trim(),StringComparison.OrdinalIgnoreCase))
            {
                return CreateSqlDatabase(connectionString);
            }

            if ("System.Data.OracleClient".Equals(providerName.Trim(), StringComparison.OrdinalIgnoreCase) || "Oracle.DataAccess.Client".Equals(providerName.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return CreateOracleDatabase(connectionString);
            }

            if (providerName.StartsWith("System.Data.SqlServerCe", StringComparison.OrdinalIgnoreCase))
            {
                return CreateSqlCeDatabase(connectionString);
            }

            if (providerName.StartsWith("MySql.Data.MySqlClient", StringComparison.OrdinalIgnoreCase))
            {
                return CreateMySqlDatabase(connectionString);
            }

            throw new DaoException(String.Format("please check provider name '{0}' is correct,now only suport sql server,oracle,sql server ce", providerName));
        }

        private static Database CreateSqlDatabase(string connectionString)
        {
            return new SqlDatabase(connectionString);
        }

        private static Database CreateOracleDatabase(string connectionString)
        {
            return new OracleDatabase(connectionString);
        }

        private static Database CreateSqlCeDatabase(string connectionString)
        {
            return new SqlCeDatabase(connectionString);
        }

        private static Database CreateMySqlDatabase(string connectionString)
        {
            //http://entlibcontrib.codeplex.com/releases/view/69407
            return new MySqlDatabase(connectionString);
            // TODO：暂时先使用反射，是为了避免编译时依赖
            /*DbProviderFactory dbProviderFactory = null;
            Assembly a1 = Assembly.Load("MySql.Data");
            
            Type type = a1.GetType("MySql.Data.MySqlClient.MySqlClientFactory");
            
            dbProviderFactory = (DbProviderFactory)type.GetField("Instance").GetValue(null);

            return new GenericDatabase(connectionString, dbProviderFactory);*/
        }

        private static void Initialize()
        {
            //必须先初始化ActionExecutor，因为在加载Sql时需要引用
            InitializeActionExecutors();

            InitializeProviders();

            InitializeMappingProviders();

            InitializeParameterResolvers();

            _daos = new Dictionary<string, Dao>();
            _sqlSource = GetObjectOrDefault(() => new SqlSource().LoadSqls());
        }

        private static void InitializeProviders()
        {
            _providers = new Dictionary<string, IDaoProvider>();
            _providerMapping = new Dictionary<string, IDaoProvider>();

            //内置的Provider
            DaoProvider.BuiltinProviders.ForEach(provider => _providers.Add(provider.Name, provider));

            //配置的Provider
            ObjectHelper.GetAllObjects<IDaoProvider>()
                                       .ForEach(provider => _providers.Add(provider.Name, provider));
        }

        private static void InitializeMappingProviders()
        {
            _mappingProviders = new LinkedList<IMappingProvider>();

            //内置
            TableMapper.BuiltinProviders.ForEach(provider => _mappingProviders.AddFirst(provider));

            //配置
            ObjectHelper.GetAllObjects<IMappingProvider>()
                                        .ForEach(provider => _mappingProviders.AddFirst(provider));
        }

        private static void InitializeParameterResolvers()
        {
            _parameters = ObjectHelper.GetAllObjects<ISqlParameters>();

            if (_parameters.Count() > 0 && log.IsDebugEnabled)
            {
                _parameters.ForEach(parameter =>
                    log.Debug("Found Parameter Type : {0}", parameter.GetType().FullName));
            }
        }

        private static void InitializeActionExecutors()
        {
            _actionExecutors = ObjectHelper.GetNamedObjectCollection<ISqlActionExecutor>();

            log.Debug("Found {0} Action Executors", _actionExecutors.Count);
        }

        private static T GetObjectOrDefault<T>(Func<T> CreateDefault)
        {
            T obj;
            if (!ObjectHelper.TryGetObject<T>(out obj))
            {
                return CreateDefault();
            }
            return obj;
        }
    }
}