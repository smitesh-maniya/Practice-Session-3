using System.Diagnostics;

namespace PraticeSessionDemo.Middleware
{
    public class LogTimeTakingByAPI
    {
        RequestDelegate _next;
        private ILogger<LogTimeTakingByAPI> _logger;

        public LogTimeTakingByAPI(ILogger<LogTimeTakingByAPI> logger, RequestDelegate next) 
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var responseStream = context.Response.Body;
            using var buffer = new MemoryStream();
            context.Response.Body = buffer;
            await _next(context);

            stopWatch.Stop();

            buffer.Seek(0, SeekOrigin.Begin);
            var responseContent = new StreamReader(buffer).ReadToEnd();
            _logger.LogInformation(responseContent);

            // Copy the buffered response back to the original response stream
            buffer.Seek(0, SeekOrigin.Begin);
            await buffer.CopyToAsync(responseStream);
            context.Response.Body = responseStream;

            _logger.LogInformation("Request {Method} {Path} took {ElapsedMilliseconds} ms",
            context.Request.Method,
            context.Request.Path,
            stopWatch.ElapsedMilliseconds);
        }

    }
}
