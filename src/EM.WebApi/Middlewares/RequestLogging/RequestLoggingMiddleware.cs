using System.Text;

namespace EM.WebApi.Middlewares.RequestLogging
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            bool isSwaggerRelatedRequest = httpContext.Request.Path.ToString().StartsWith("/swagger/");

            var threadId = Environment.CurrentManagedThreadId;
            var canLog = !isSwaggerRelatedRequest;
            if (canLog)
            {
                StringBuilder loggingData;
                string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
                string? clientPort = httpContext.Connection.RemotePort.ToString();

                HttpRequest request = httpContext.Request;

                request.EnableBuffering();

                using (StreamReader reader = new(request.Body, Encoding.UTF8, true, 1024, true))
                {
                    loggingData = new StringBuilder(await reader.ReadToEndAsync());
                }

                if (!string.IsNullOrEmpty(httpContext.Request.QueryString.Value))
                {
                    loggingData.Append(httpContext.Request.QueryString.Value);
                }

                request.Body.Position = 0;
                _logger.LogInformation($"client ip: {clientIp}; client port: {clientPort}; requestedUrl: {request.Path}; requestMethod: {request.Method}; requestData: {loggingData}");
            }

            await _next(httpContext);
        }

    }
}
