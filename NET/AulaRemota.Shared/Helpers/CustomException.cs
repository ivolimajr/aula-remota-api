using AulaRemota.Shared.Helpers.Constants;
using System;
using System.Net;

namespace AulaRemota.Shared.Helpers
{
    public class CustomException : Exception
    {
        public ResponseModel ResponseModel { get; set; }
        public CustomException()
        {
        }
        public CustomException(string userMessage, string modelName, Exception exception) : base(exception.Message, exception.InnerException)
        {
            var t = exception.GetType();
            ResponseModel = new ResponseModel();
            ResponseModel.ModelName = modelName;
            ResponseModel.UserMessage = userMessage;
            ResponseModel.ExceptionMessage = exception.Message;
            ResponseModel.InnerException = exception.InnerException;
            ResponseModel.InnerExceptionMessage = exception.InnerException?.Message;

            if (ResponseModel.StatusCode <= 0)
                ResponseModel.StatusCode = ((HttpWebResponse)(exception as WebException)?.Response != null)
                ? ((HttpWebResponse)(exception as WebException).Response).StatusCode
                : GetErrorCode(exception.GetType(), exception.GetType().Name == nameof(CustomException) ? (CustomException)exception : exception);
        }

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
        private static HttpStatusCode GetErrorCode(Type exceptionType, Exception exception)
        {
            Exceptions tryParseResult;
            if (Enum.TryParse<Exceptions>(exceptionType.Name, out tryParseResult))
            {
                return tryParseResult switch
                {
                    Exceptions.NullReferenceException => HttpStatusCode.LengthRequired,
                    Exceptions.FileNotFoundException => HttpStatusCode.NotFound,
                    Exceptions.OverflowException => HttpStatusCode.RequestedRangeNotSatisfiable,
                    Exceptions.OutOfMemoryException => HttpStatusCode.ExpectationFailed,
                    Exceptions.InvalidCastException => HttpStatusCode.PreconditionFailed,
                    Exceptions.ObjectDisposedException => HttpStatusCode.Gone,
                    Exceptions.UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                    Exceptions.NotImplementedException => HttpStatusCode.NotImplemented,
                    Exceptions.NotSupportedException => HttpStatusCode.NotAcceptable,
                    Exceptions.InvalidOperationException => HttpStatusCode.MethodNotAllowed,
                    Exceptions.TimeoutException => HttpStatusCode.RequestTimeout,
                    Exceptions.ArgumentException => HttpStatusCode.BadRequest,
                    Exceptions.StackOverflowException => HttpStatusCode.RequestedRangeNotSatisfiable,
                    Exceptions.FormatException => HttpStatusCode.UnsupportedMediaType,
                    Exceptions.IOException => HttpStatusCode.NotFound,
                    Exceptions.IndexOutOfRangeException => HttpStatusCode.ExpectationFailed,
                    Exceptions.CustomException => ((CustomException)exception).ResponseModel != null && ((CustomException)exception).ResponseModel.StatusCode.HasValue ? ((CustomException)exception).ResponseModel.StatusCode.Value : HttpStatusCode.BadRequest,
                    _ => HttpStatusCode.InternalServerError
                };
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
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
