using System.Text.Json;

namespace ExamLayer.Middlewares
{
    public class ResponseHandler
    {
        private readonly RequestDelegate _next;

        public ResponseHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originBody = context.Response.Body;
            try
            {
                var memStream = new MemoryStream();
                context.Response.Body = memStream;

                await _next(context).ConfigureAwait(false);

                memStream.Position = 0;
                var responseBody = new StreamReader(memStream).ReadToEnd();

                //Custom logic to modify response
                //responseBody = responseBody.Replace("hello", "hi", StringComparison.InvariantCultureIgnoreCase);
                responseBody = JsonSerializer.Serialize(new
                {
                    ErrorCode = "0",
                    ErrorMessage = "",
                    Result = responseBody
                });

                var memoryStreamModified = new MemoryStream();
                var sw = new StreamWriter(memoryStreamModified);
                sw.Write(responseBody);
                sw.Flush();
                memoryStreamModified.Position = 0;

                await memoryStreamModified.CopyToAsync(originBody).ConfigureAwait(false);
            }
            finally
            {
                context.Response.Body = originBody;
            }
        }
    }
}
