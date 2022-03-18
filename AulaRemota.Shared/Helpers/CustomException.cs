using System;
using System.Net;

namespace AulaRemota.Shared.Helpers
{
    public class CustomException : Exception
    {
        public ResponseModel ResponseModel { get; set; }

        public CustomException(ResponseModel responseModel) : base(responseModel.UserMessage ?? responseModel.ExceptionMessage, responseModel.InnerException)
        {
            ResponseModel = responseModel;
        }

        public CustomException(string message, HttpStatusCode? statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            ResponseModel = new ResponseModel
            {
                UserMessage = message,
                StatusCode = statusCode
            };
        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class ResponseModel
    {
        public string UserMessage { get; set; }
        public string ExceptionMessage { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public string ModelName { get; set; }
        public Exception Exception { get; set; }
        public Exception InnerException { get; set; }
        public string InnerExceptionMessage { get; set; }
        public object Data { get; set; }
    }
}
