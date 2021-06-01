using Docker.DotNet;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Redpier.Web.API.Extensions
{
    public static class ExceptionExtensions
    {
        public static IEnumerable<Exception> GetInnerExceptions(this Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var innerException = exception;

            do
            {
                yield return innerException;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }

        public static string GetExceptionDetails(this DockerApiException exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var details = string.Empty;

            try
            {
                var obj = JsonSerializer.Deserialize<JsonElement>(exception.ResponseBody);
                details = obj.Get("message").Value.GetString();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while deserializing json.");
            }

            return details;
        }
    }
}
