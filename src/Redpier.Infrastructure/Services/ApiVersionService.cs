using Redpier.Application.Common.Interfaces;
using System.Diagnostics;
using System.Reflection;

namespace Redpier.Infrastructure.Services
{
    public class ApiVersionService : IApiVersionService
    {
        public string Version => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
    }
}
