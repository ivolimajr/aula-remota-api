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
        private readonly ILogger<ErrorMiddleware> Logger;

        public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
        {
            this.next = next;
            Logger = logger;
        }
        protected Func<ResponseModel, string> DefaultFormatter()
        {
            return (logData => $"\x1B[44m {logData.UserMessage} - {logData.ModelName} - {logData.StatusCode} - {logData.InnerException}ms \x1B[40m");
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
            ex.ResponseModel.InnerExceptionMessage = ex.InnerException?.Message ?? null;
            object result = null;

            string userMessage;
            string modelName;
            string dataException = default;


            switch (ex)
            {
                case CustomException customException:
                    userMessage = customException.ResponseModel.ModelName ?? null;
                    modelName = customException.ResponseModel.ModelName ?? null;
                    if (customException.ResponseModel.Data != null) dataException = customException.ResponseModel.Data.ToString();
                    break;
            }

            if (ex.ResponseModel.Exception !=null && ex.ResponseModel.Exception.InnerException != null)
            {
                result = new
                {
                    InnerExceptionMessage = ex.ResponseModel.Exception.InnerException.Message ?? null,
                    ModelName = ex.ResponseModel.ModelName ?? null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            else
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

            #region Logging

            Logger.LogError(new
            {
                result = result,
                data = dataException
            }.ToString());

            #endregion Logging

            var jsonResult = JsonConvert.SerializeObject(result);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(jsonResult);
        }
    }
}