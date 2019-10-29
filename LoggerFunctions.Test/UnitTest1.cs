using Api.Models;
using Controllers;
using Core.Configurations;
using Core.Logic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            LoggerConfigurator loggerConfigurator = new LoggerConfigurator();
            LoggerFunctions loggerFunctions = new LoggerFunctions(loggerConfigurator);
            LogMessage lm1 = new LogMessage
            {
                Computer = "Comp1",
                User = "User1",
                Logged = DateTime.Now,
                LogName = "lofName",
                Application = "App",
                Sourse = "qweqweqweqe",
                EventID = 1234,
                Keywords = "qwe",
                Level = Level.Critical,
                Message = "qweqweqweqe",
                TaskCategory = "None"
            };
            loggerFunctions.Report(lm1);
            LogMessage lm = new LogMessage
            {
                Computer = "Comp",
                User = "User",
                Logged = DateTime.Now,
                LogName = "lofName",
                Application = "App",
                Sourse = "Sourse1",
                EventID = 1234,
                Keywords = "qwe",
                Level = Level.Warning,
                Message = "Message",
                TaskCategory = "None"
            };

            loggerFunctions.Report(lm);

            StringBuilder result = new StringBuilder();
            switch (lm.Level)
            {
                case Level.Warning:
                    result.Append("WARN | ");
                    break;
                case Level.Error:
                    result.Append("ERROR | ");
                    break;
                case Level.Critical:
                    result.Append("FATAL | ");
                    break;
                default:
                    result.Append("INFO | ");
                    break;
            }
            result.Append($"{lm.Computer} | {lm.User} | {lm.Logged} | {lm.Application} | {lm.Sourse} | {lm.EventID} | {lm.LogName} | {lm.TaskCategory} | {lm.Keywords} | {lm.Message}");

            string filepath = Path.Combine(Environment.CurrentDirectory, $@"logs\{DateTime.Now.Date.ToString("yyyy-MM-dd")}.log");
            List<string> list = new List<string>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                while (!sr.EndOfStream)
                {
                    list.Add(sr.ReadLine());
                }
            }
            bool found = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Contains(result.ToString()))
                {
                    found = true;
                }
            }
            Assert.AreEqual(found, true);
        }

        [Test]
        public async Task TestReportAsync()
        {
            LoggerConfigurator loggerConfigurator = new LoggerConfigurator();
            LoggerFunctions loggerFunctions = new LoggerFunctions(loggerConfigurator);
            LogController logController = new LogController(loggerFunctions);
            List<LogMessage> logMessages = new List<LogMessage>();
            Random = new Random();
            Level level = Level.Null;
            for (int i = 0; i < 100; i++)
            {
                if (i % 4 == 1)
                {
                    level = Level.Warning;
                }
                else if (i % 4 == 2)
                {
                    level = Level.Error;
                }
                else if (i % 4 == 3)
                {
                    level = Level.Critical;
                }
                else
                {
                    level = Level.Information;
                }
                logMessages.Add(new LogMessage
                {
                    Computer = RandomString(5),
                    User = RandomString(6),
                    Logged = DateTime.Now,
                    LogName = RandomString(7),
                    Application = "App",
                    Sourse = RandomString(8),
                    EventID = i,
                    Keywords = "qwe",
                    Level = level,
                    Message = "qweqweqweqe",
                    TaskCategory = "None"
                });
            }

            /*Parallel.For(0, 99, async (i) =>
            {
                await logController.ReportAsync(logMessages[i]);
            });*/
            /*Parallel.For(0, 99, (i) =>
            {
                logController.Report(logMessages[i]);
            });*/
            for (int i = 0; i < 100; i++)
            {
                logController.ReportAsync(logMessages[i]);
            }
        }

        static Random Random;
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}