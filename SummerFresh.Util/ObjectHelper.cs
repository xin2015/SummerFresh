using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;

namespace SummerFresh.Util
{
    public class ObjectHelper
    {
        private static IUnityContainer _container;
        private static readonly ILog log = LogManager.GetCurrentClassLogger();
        private const string UNITY_DIRECTORY_NAME = "Unity";

        static ObjectHelper()
        {
            _container = InitUnityContainer();
        }

        public static void Register<TType>(TType instance)
        {
            Register(typeof(TType), instance);
        }

        public static void Register<TType>(string name, TType instance)
        {
            Register(typeof(TType), name, instance);
        }

        public static void Register(Type type, object instance)
        {
            _container.RegisterInstance(type, instance);
        }

        public static void Register(Type type, string name, object instance)
        {
            _container.RegisterInstance(type, name, instance);
        }

        public static object GetObject(string name)
        {
            object obj;
            if (!TryGetObject(name, out obj))
            {
                throw new ObjectNotFoundException(string.Format("No Object Named '{0}' Registered", name));
            }
            return obj;
        }

        public static bool TryGetObject(string name, out object obj)
        {
            IList<Type> types = (from reg in _container.Registrations
                                 where name.Equals(reg.Name)
                                 select reg.RegisteredType).ToList();

            if (types.Count == 0)
            {
                obj = null;
                return false;
            }
            else if (types.Count == 1)
            {
                return TryGetObject(types[0], name, out obj);
            }
            else
            {
                throw new InvalidOperationException(
                    string.Format("Multiple objects with name '{0}' has been found", name));
            }
        }

        public static TType GetObject<TType>(string name)
        {
            TType obj;

            if (!TryGetObject<TType>(name, out obj))
            {
                throw new ObjectNotFoundException(string.Format("No Object Named '{0}' of Type '{1}' Registered", name, typeof(TType).FullName));
            }
            return obj;
        }

        public static TType GetObject<TType>()
        {
            TType obj;

            if (!TryGetObject<TType>(out obj))
            {
                throw new ObjectNotFoundException(string.Format("No Object of Type '{0}' Registered", typeof(TType).FullName));
            }
            return obj;
        }

        public static bool TryGetObject<TType>(out TType obj)
        {
            if (!_container.IsRegistered(typeof(TType)))
            {
                obj = default(TType);
                return false;
            }
            else
            {
                obj = _container.Resolve<TType>();
                return true;
            }
        }

        public static bool TryGetObject<TType>(string name, out TType obj)
        {
            if (!_container.IsRegistered(typeof(TType), name))
            {
                obj = default(TType);
                return false;
            }
            else
            {
                obj = _container.Resolve<TType>(name);
                return true;
            }
        }


        public static object GetObject(Type type)
        {
            object obj;
            if (!TryGetObject(type, out obj))
            {
                throw new ObjectNotFoundException(string.Format("No Object of Type '{0}' Registered", type.FullName));
            }
            return obj;
        }

        public static object GetObject(Type type, string name)
        {
            object obj;
            if (!TryGetObject(type, name, out obj))
            {
                throw new ObjectNotFoundException(string.Format("No Object Named '{0}' of Type '{1}' Registered", name, type.FullName));
            }
            return obj;
        }

        public static bool TryGetObject(Type type, out object obj)
        {
            if (!_container.IsRegistered(type))
            {
                obj = null;
                return false;
            }
            else
            {
                obj = _container.Resolve(type);
                return true;
            }
        }

        public static bool TryGetObject(Type type, string name, out object obj)
        {
            if (!_container.IsRegistered(type, name))
            {
                obj = null;
                return false;
            }
            else
            {
                obj = _container.Resolve(type, name);
                return true;
            }
        }

        public static IEnumerable<object> GetAllObjects(Type type)
        {
            object defaultInstance;
            IEnumerable<object> namedInstance = GetNamedObjects(type);

            if (!TryGetObject(type, out defaultInstance))
            {
                return namedInstance;
            }
            else
            {
                return namedInstance.Concat(new object[] { defaultInstance });
            }
        }

        public static IEnumerable<TType> GetAllObjects<TType>()
        {
            TType defaultInstance;
            IEnumerable<TType> namedInstance = GetNamedObjects<TType>();

            if (!TryGetObject<TType>(out defaultInstance))
            {
                return namedInstance;
            }
            else
            {
                return namedInstance.Concat(new TType[] { defaultInstance });
            }
        }

        public static IEnumerable<TType> GetNamedObjects<TType>()
        {
            return _container.ResolveAll<TType>();
        }

        public static IEnumerable<object> GetNamedObjects(Type type)
        {
            return _container.ResolveAll(type);
        }

        public static NameObjectCollection<TType> GetNamedObjectCollection<TType>()
        {
            Type type = typeof(TType);
            NameObjectCollection<TType> collection = new NameObjectCollection<TType>();

            foreach (var reg in _container.Registrations)
            {
                if (reg.RegisteredType.Equals(type) && !string.IsNullOrEmpty(reg.Name))
                {
                    collection.Add(reg.Name, (TType)_container.Resolve(type, reg.Name));
                }
            }

            return collection;
        }

        public static NameObjectCollection<object> GetNamedObjectCollection(Type type)
        {
            NameObjectCollection<object> collection = new NameObjectCollection<object>();

            foreach (var reg in _container.Registrations)
            {
                if (reg.RegisteredType.Equals(type) && !string.IsNullOrEmpty(reg.Name))
                {
                    collection.Add(reg.Name, _container.Resolve(type, reg.Name));
                }
            }

            return collection;
        }


        private static IUnityContainer InitUnityContainer()
        {
            IUnityContainer container = new UnityContainer();

            //Found Unity Config Directory,Load All Configuration Files
            DirectoryInfo dir = null;

            AppUtility.FindConfigDirectory(UNITY_DIRECTORY_NAME,out dir);

            IEnumerable<FileInfo> files = LoadConfigurations(dir);

            log.Debug("Load {0} Unity Configuration Files in dir '{1}'", files.Count(), dir);

            IEnumerable<Assembly> assemblies = Assemblies.GetAssemblies();

            foreach (FileInfo file in files)
            {
                try
                {
                    ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = file.FullName };

                    System.Configuration.Configuration config =
                        ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

                    UnityConfigurationSection section = config.GetSection("unity") as UnityConfigurationSection;
                    if (null != section)
                    {
                        if (section.Containers.Count > 1)
                        {
                            throw new Exception(String.Format("Multiple '<container>' Element Not Supported in {0}", file.FullName));
                        }

                        if (section.Containers.Count != 0)
                        {
                            if (!string.IsNullOrEmpty(section.Containers[0].Name))
                            {
                                //TODO : should throw an exception ?
                                section.Containers[0].Name = null;
                            }

                            log.Debug("Config Unity File : {0}", file.Name);

                            //自动注册所有加载的Assemblies
                            //Unity每次执行Configure都替换当前线程的TypeResolver的内容，所以需要重新注册
                            foreach (var assembly in assemblies)
                            {
                                section.Assemblies.Add(new AssemblyElement() { Name = assembly.GetName().Name });
                            }

                            section.Configure(container);
                        }
                        else
                        {
                            log.Trace("Ignore Empty Config : '{0}'", file.FullName);
                        }
                    }
                }
                catch (Exception e)
                {
                    string message = string.Format("Load Unity Config '{0}' Error : {1}", file.Name, e.Message);

                    log.Info(message, e);

                    throw new Exception(message, e);
                }
            }

            //IUnityContainer itself Registered,so the configured types should be 'Registrations.Count() - 1'
            log.Debug("Load {0} Registered Objects by Unity Container", container.Registrations.Count() - 1);

            return container;
        }

        private static IEnumerable<FileInfo> LoadConfigurations(DirectoryInfo dir)
        {
            return dir.GetFiles("*.config", SearchOption.AllDirectories);
        }

    }
    public class NameObjectCollection<T> : NameObjectCollectionBase
    {
        public void Add(string name, T value)
        {
            BaseAdd(name, value);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public T this[string name]
        {
            get { return (T)BaseGet(name); }

            set { BaseSet(name, value); }
        }
    }

    public sealed class ObjectNotFoundException : Exception
    {
        internal ObjectNotFoundException()
            : base()
        {

        }

        internal ObjectNotFoundException(string message)
            : base(message)
        {

        }

        internal ObjectNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

    public static class Assemblies
    {
        private static readonly ReadOnlyCollection<Assembly> _all = null;
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        static Assemblies()
        {
            List<Assembly> all = new List<Assembly>();
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                //这里只加载了当前运行所需要的程序集，未必是全部。
                AddAssembly(all, a);
            }
            if (HttpContext.Current != null)
            {
                foreach (Assembly a in BuildManager.GetReferencedAssemblies())
                {
                    if (!all.Any(loaded =>
                        AssemblyName.ReferenceMatchesDefinition(loaded.GetName(), a.GetName())))
                    {
                        AddAssembly(all, a);
                    }
                }
                string binDir = HttpRuntime.BinDirectory;
                if (!string.IsNullOrEmpty(binDir))
                {
                    string[] files = Directory.GetFiles(binDir, "*.dll", SearchOption.TopDirectoryOnly);
                    foreach (string file in files)
                    {
                        if (file.StartsWith("SummerFresh", StringComparison.OrdinalIgnoreCase))
                        {
                            AssemblyName name = AssemblyName.GetAssemblyName(file);
                            Assembly a = Assembly.Load(name);

                            if (!all.Any(loaded =>
                                AssemblyName.ReferenceMatchesDefinition(loaded.GetName(), name)))
                            {
                                AddAssembly(all, a);
                            }
                        }
                    }
                }
            }
            _all = new ReadOnlyCollection<Assembly>(all);
        }

        private static void AddAssembly(List<Assembly> all, Assembly a)
        {
            if (a.FullName.StartsWith("SummerFresh", StringComparison.OrdinalIgnoreCase))
            {
                log.Debug("Load assembly : {0}", a.GetName().Name);
                all.Add(a);
            }
        }

        public static IEnumerable<Assembly> GetAssemblies()
        {
            return _all;
        }
    }
}
