using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Models;
using Core.Configurations;

namespace Core.Logic
{
    public class LoggerFunctions : ILoggerFunctions
    {
        public readonly LoggerConfigurator _loggerConfigurator;
        public LoggerFunctions(LoggerConfigurator LoggerConfig)
        {
            _loggerConfigurator = LoggerConfig;
        }

        public void Report(LogMessage message)
        {
            string result = $"{message.Computer} | {message.User} | {message.Logged} | {message.Application} | {message.Sourse} | {message.EventID} | {message.LogName} | {message.TaskCategory} | {message.Keywords} | {message.Message}";
            switch (message.Level)
            {
                case Level.Warning:
                    _loggerConfigurator.logger.Warn(result);
                    break;
                case Level.Error:
                    _loggerConfigurator.logger.Error(result);
                    break;
                case Level.Critical:
                    _loggerConfigurator.logger.Fatal(result);
                    break;
                default:
                    _loggerConfigurator.logger.Info(result);
                    break;
            }
        }
    }
}
