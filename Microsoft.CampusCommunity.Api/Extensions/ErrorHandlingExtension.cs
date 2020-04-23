using Microsoft.AspNetCore.Builder;
using Microsoft.CampusCommunity.Infrastructure.Middleware;

namespace Microsoft.CampusCommunity.Api.Extensions
{
    internal static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}