using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DotnetCoreDapperSample.Api.Middlewares
{
    /// <summary>
    /// アクセスログを記録するためのミドルウェアです。
    /// 액세스 로그를 기록하기 위한 미들웨어입니다.
    /// </summary>
    public class AccessLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public AccessLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<AccessLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                _logger.LogInformation(
                    "Request {method} {url} => {statusCode}",
                    context.Request.Method,
                    context.Request.Path.Value,
                    context.Response?.StatusCode);
            }
        }
    }

    public static class AccessLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseAccessLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AccessLoggingMiddleware>();
        }
    }
}
