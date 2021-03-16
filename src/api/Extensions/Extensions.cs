using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.API.Extensions
{
    public static class Extensions
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
    }
}
