using Api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface ILoggerFunctions
    {
        void Report(LogMessage message);
    }
}
