using ExamLayer.Enums;
using ExamLayer.Exceptions;
using System.Net;
using System.Text.Json;

namespace ExamLayer.Middlewares
{
    public class ApiExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ApiExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalResponseBodyStream = context.Response.Body;
            await using var newResponseBodyStream = new MemoryStream();
            context.Response.Body = newResponseBodyStream;

            try
            {
                await _next(context);
                //取body出來判斷
                newResponseBodyStream.Seek(0, SeekOrigin.Begin);
                var responseData = await new StreamReader(newResponseBodyStream).ReadToEndAsync();
                //if (context.Response.StatusCode == 401)
                //    throw new ApiException(ApiEnum.ErrorCode.TokenVaildFail);
                if (context.Response.StatusCode == 400 && responseData.Contains("The input field is required"))
                    throw new ApiException(ApiEnum.ErrorCode.RequiredFieldParam);

                //將原body回復
                newResponseBodyStream.Seek(0, SeekOrigin.Begin);
                await newResponseBodyStream.CopyToAsync(originalResponseBodyStream);
                context.Response.Body = originalResponseBodyStream;
            }
            catch (Exception error)
            {
                context.Response.ContentType = "application/json";
                int errorCode = (int)ApiEnum.ErrorCode.UncatchException;
                switch (error)
                {
                    case ApiException e:
                        context.Response.StatusCode =
                            e.ErrorCode == ApiEnum.ErrorCode.TokenVaildFail || e.ErrorCode == ApiEnum.ErrorCode.LoginFail ? 401
                            : (e.ErrorCode == ApiEnum.ErrorCode.AccNotFound || e.ErrorCode == ApiEnum.ErrorCode.AccInvalid
                                || e.ErrorCode == ApiEnum.ErrorCode.AccLocked || e.ErrorCode == ApiEnum.ErrorCode.AccInActive
                                || e.ErrorCode == ApiEnum.ErrorCode.FeatureNotUse ? 403
                            : (e.ErrorCode == ApiEnum.ErrorCode.RequiredFieldParam ? 400
                            : 500));
                        errorCode = (int)e.ErrorCode;
                        break;
                    case KeyNotFoundException e:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new
                {
                    errorCode = errorCode,
                    errorMessage = error?.Message
                });

                context.Response.Body = originalResponseBodyStream;
                await context.Response.WriteAsync(result);
            }
        }
    }
}
