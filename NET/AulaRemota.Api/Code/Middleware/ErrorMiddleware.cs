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

            ex.ResponseModel.InnerExceptionMessage= ex.InnerException?.Message ?? null;
            object result = null;

            string userMessage;
            string modelName;


            switch (ex)
            {
                case CustomException customException:
                    userMessage = customException.ResponseModel.ModelName ?? null;
                    modelName = customException.ResponseModel.ModelName ?? null;
                    break;
            }

            if (ex.ResponseModel.Exception.InnerException != null)
            {
                result = new
                {
                    InnerExceptionMessage = ex.ResponseModel.Exception.InnerException.Message ?? null,
                    ModelName = ex.ResponseModel.ModelName ?? null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            } else
            {
                result = new
                {
                    UserMessage = ex.ResponseModel.UserMessage ?? null,
                    ModelName = ex.ResponseModel.ModelName ?? null,
                    statusCode = ex.ResponseModel.StatusCode
                };
            }
            ex.ResponseModel.Exception = null;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            _logger.LogInformation(this.logLineFormatter(ex.ResponseModel));

            var jsonResult = JsonConvert.SerializeObject(result);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(jsonResult);
        }
    }
}