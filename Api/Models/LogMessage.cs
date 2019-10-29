using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Models
{
    public class LogMessage
    {
        public string Computer { get; set; }
        public string User { get; set; }
        public DateTime Logged { get; set; }
        public string Application { get; set; }
        public string Sourse { get; set; }
        public int EventID { get; set; }
        public string LogName { get; set; }
        public string TaskCategory { get; set; }
        public Level Level { get; set; }
        public string Keywords { get; set; }
        public string Message { get; set; }
    }
}
