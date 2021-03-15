using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Redpier.Infrastructure.Identity;
using Redpier.Infrastructure.Persistence.Context;
using Redpier.Infrastructure.Persistence;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Redpier.Web.API
{
    public class Program
    {
        public async static Task<int> Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            try
            {
                Log.Information("Starting Redpier API.");
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    try
                    {
                        var context = services.GetRequiredService<ApplicationDbContext>();

                        await context.Database.MigrateAsync();

                        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                        await ApplicationDbContextSeed.SeedDefaultRolesAsync(roleManager);
                        await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager);


                        Log.Information("Database seeded with default values.");

                    }

                    catch (Exception ex)
                    {
                        Log.Error(ex, "An error occurred while migrating or seeding the database.");
                    }
                }

                await host.RunAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");

                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(new ConfigurationBuilder()
                       .AddCommandLine(args)
                       .Build());
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog();
                });
    }
}
