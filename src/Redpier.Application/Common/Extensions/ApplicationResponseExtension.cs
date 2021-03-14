using Microsoft.AspNetCore.Builder;
using Redpier.Application.Common.Middlewares;

namespace Redpier.Application.Common.Extensions
{
    public static class ApplicationResponseExtension
    {
        public static IApplicationBuilder UseApplicationResponse(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApplicationResponseMiddleware>();
        }
    }
}
