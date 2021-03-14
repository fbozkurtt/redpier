namespace Redpier.Application.Common.Models
{
    public class ApplicationResponse<TResponse>
    {
        public TResponse Response { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }
    }
}
