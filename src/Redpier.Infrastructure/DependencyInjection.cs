using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redpier.Application.Common.Interfaces;
using Redpier.Infrastructure.Identity;
using Redpier.Infrastructure.Persistence.Context;
using Redpier.Infrastructure.Services;
using System;

namespace Redpier.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string migrationAssembly = typeof(ApplicationDbContext).Assembly.FullName;

            var connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = configuration.GetSection("SQLite")["FilePath"],
                Password = configuration.GetSection("SQLite")["Password"]
            };

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    connectionString.ToString(),
                    b => b.MigrationsAssembly(migrationAssembly)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);

                    options.Password.RequiredLength = 5;
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 0;

                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}