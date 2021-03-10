using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Redpier.Application.Common.Interfaces;
using Redpier.Infrastructure.Persistence.Context;
using Redpier.Infrastructure.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Redpier.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
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

            services.AddAuthentication();

            return services;
        }
    }
}