using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Redpier.Application.Common.Interfaces;
using Redpier.Application.Common.Interfaces.Identity;
using Redpier.Application.Common.Interfaces.Repositories;
using Redpier.Domain.Common;
using Redpier.Infrastructure.Identity;
using Redpier.Infrastructure.Persistence.Context;
using Redpier.Infrastructure.Persistence.Repositories;
using Redpier.Infrastructure.Services;

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
                    "Data source=RedpierDb.db",
                    b => b.MigrationsAssembly(migrationAssembly)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IRepositoryBase<BaseEntity>, RepositoryBase<BaseEntity>>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication();

            return services;
        }
    }
}