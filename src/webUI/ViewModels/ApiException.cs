using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.UI.ViewModels
{
    public class ApiExceptionResponse
    {
        public int? Status { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public IDictionary<string, string[]> Errors { get; set; }
    }
}
