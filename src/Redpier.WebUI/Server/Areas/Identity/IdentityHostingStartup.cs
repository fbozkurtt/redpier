using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redpier.Infrastructure.Identity;
using Redpier.Infrastructure.Persistence.Context;

[assembly: HostingStartup(typeof(Redpier.WebUI.Server.Areas.Identity.IdentityHostingStartup))]
namespace Redpier.WebUI.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}