using Microsoft.AspNetCore.Http;
using Redpier.Application.Common.Interfaces;
using System.Security.Claims;

namespace Redpier.Web.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => (_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));

        public string Username => (_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name));
    }
}
