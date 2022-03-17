using log4net;
using log4net.Appender;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace AulaRemota.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class ErrorLoggingController : ControllerBase
    {
        public ErrorLoggingController()
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            string path = (LogManager.GetCurrentLoggers()[0].Logger.Repository.GetAppenders()[0] as FileAppender).File;
            var result = new FileStreamResult(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), "binary/octet-stream");
            result.FileDownloadName = "Logs.log";
            return result;
        }
    }
}
