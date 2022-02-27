using AulaRemota.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AulaRemota.Api.Code.Middleware
{
    public class ErrorMiddleware

    {
        private readonly RequestDelegate next;
        private readonly ILogger _logger;

        public ErrorMiddleware(RequestDelegate next, ILoggerFactory factory)
        {
            this.next = next;
            _logger = factory.CreateLogger("RequestLog");
        }

        private Func<ResponseModel, string> _logLineFormatter;
        private Func<ResponseModel, string> logLineFormatter
        {
            get
            {

                if (this._logLineFormatter != null)
                {
                    return this._logLineFormatter;
                }
                return this.DefaultFormatter();
            }
            set
            {
                this._logLineFormatter = value;
            }
        }
        protected Func<ResponseModel, string> DefaultFormatter()
        {
            return (logData => $"\x1B[44m {logData.UserMessage} - {logData.ModelName} - {logData.StatusCode} - {logData.InnerException}ms \x1B[40m");
        }
        public void SetLogLineFormat(Func<ResponseModel, string> formatter)
        {
            this._logLineFormatter = formatter;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (CustomException customException)
            {
                await HandleExceptionAsync(context, customException);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, CustomException ex)
        {
            //TODO: Gravar log de erro com o trace id

            CustomException customException;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ||
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Qa")
            {
                if (ex.ResponseModel != null) customException = new CustomException(ex.ResponseModel);
                else customException = new CustomException(ex.Message, ex.InnerException);

                if(ex.ResponseModel.UserMessage == null || ex.ResponseModel.StatusCode == null)
                {
                    ex.ResponseModel.InnerExceptionMessage = ex.ResponseModel.Exception.InnerException.Message;
                    ex.ResponseModel.UserMessage = ex.ResponseModel.Exception.InnerException.Message;
                    ex.ResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                if (ex.ResponseModel != null) customException = new CustomException(ex.ResponseModel);
                else customException = new CustomException(ex.Message, ex.InnerException); ;
            }

            _logger.LogInformation(this.logLineFormatter(ex.ResponseModel));

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(customException.ResponseModel);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}