using AulaRemota.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AulaRemota.Api.Code.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorMiddleware(RequestDelegate next)
        {
            this.next = next;
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
                if(ex.ResponseModel != null) customException = new CustomException(ex.ResponseModel);
                else customException = new CustomException(ex.Message,ex.InnerException); ;
            }
            else
            {
                if (ex.ResponseModel != null) customException = new CustomException(ex.ResponseModel);
                else customException = new CustomException(ex.Message, ex.InnerException); ;
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(customException.ResponseModel);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}