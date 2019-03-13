using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerFresh.Util
{
    public static class LogHelper
    {
        private static ILog log = LogManager.GetLogger("MVCErrorLogger");

        public static ILog GetLogger(string name)
        {
            return LogManager.GetLogger(name);
        }
        public static void Info(string message)
        {
            log.Info(message);
        }

        public static void Error(string message, Exception ex)
        {
            log.Error(message, ex);
        }
    }
}
