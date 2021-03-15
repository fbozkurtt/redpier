using Microsoft.AspNetCore.Http;
using Redpier.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Middlewares
{
    public class ApplicationResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiVersionService _apiVersionService;

        public ApplicationResponseMiddleware(RequestDelegate next, IApiVersionService apiVersionService)
        {
            _next = next;
            _apiVersionService = apiVersionService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //if (true || context.Request.Path.ToString().Contains("api"))
            //{
            //    var currentBody = context.Response.Body;

            //    using var memoryStream = new MemoryStream();

            //    context.Response.Body = memoryStream;

            //    context.Response.ContentType = MediaTypeNames.Application.Json;

            //    await _next(context);

            //    context.Response.Body = currentBody;

            //    memoryStream.Seek(0, SeekOrigin.Begin);

            //    var readToEnd = new StreamReader(memoryStream).ReadToEnd();

            //    var responseObj = JsonConvert.DeserializeObject(readToEnd);

            //    var result = ApplicationResponse.Create(_apiVersionService, context.Response.StatusCode, responseObj, null);

            //    await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            //}

            //else
            await _next(context);
        }
    }
}
