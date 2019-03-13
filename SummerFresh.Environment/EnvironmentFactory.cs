using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Util;

namespace SummerFresh.Environment
{
    public static class EnvironmentFactory
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        private static IEnvironmentParser    _parser;
        private static IEnvironmentContainer _container;

        static EnvironmentFactory()
        {
            Initialize();
        }

        public static IEnvironmentContainer Container
        {
            get { return _container; }
        }

        public static IEnvironmentParser Parser
        {
            get { return _parser; }
        }

        private static void Initialize()
        {
            //需要先初始化Parser
            if (!ObjectHelper.TryGetObject<IEnvironmentParser>(out _parser))
            {
                _parser = new EnvironmentParser();
            }
            log.Debug("Using Environment Parser : {0}", _parser.GetType().FullName);

            //初始化环境变量的容器
            if (!ObjectHelper.TryGetObject<IEnvironmentContainer>(out _container))
            {
                _container = new EnvironmentContainer();
            }
            log.Debug("Using Environment Container : {0}", _container.GetType().FullName);
        }
    }
}