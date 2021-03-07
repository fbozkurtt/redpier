using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redpier.Application.Common.Interfaces;
using Redpier.Infrastructure.Identity;
using Redpier.Infrastructure.Persistence.Context;
using Redpier.Infrastructure.Services;

namespace Redpier.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetValue<bool>("UseSqlite") ? configuration.GetConnectionString("SqliteConnection") : configuration.GetConnectionString("SqlServerConnection");
            string migrationAssembly = typeof(ApplicationDbContext).Assembly.FullName;

            if (configuration.GetValue<bool>("UseSqlite"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(
                        connectionString,
                        b => b.MigrationsAssembly(migrationAssembly)));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        connectionString,
                        b => b.MigrationsAssembly(migrationAssembly)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            //services.AddAuthorization();

            return services;
        }
    }
}