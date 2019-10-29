using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Configurations
{
    public class LoggerConfigurator
    {
        //public static string ConfigFileName { get; set; } = "NLog.Config";

        public Logger logger = NLogBuilder.ConfigureNLog("NLog.Config").GetCurrentClassLogger();
    }
}
