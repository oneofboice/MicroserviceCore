using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILoggerFunctions _loggerFunctions;

        public LogController(ILoggerFunctions loggerFunctions)
        {
            _loggerFunctions = loggerFunctions;
        }

        [HttpPost]
        [Route("ReportAsync")]
        public async Task<ActionResult> ReportAsync([FromBody] LogMessage message)
        {
            try
            {
                await Task.Run(() =>
                {
                    _loggerFunctions.Report(message);
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Report")]
        public ActionResult Report([FromBody] LogMessage message)
        {
            try
            {
                _loggerFunctions.Report(message);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
