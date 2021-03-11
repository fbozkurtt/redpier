using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Redpier.Infrastructure.Persistence.Context;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Threading.Tasks;

namespace Redpier.Web.API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                   .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                   .MinimumLevel.Override("System", LogEventLevel.Warning)
                   .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                   .Enrich.FromLogContext()
                   .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                   .WriteTo.File("logs/redpierAPIlogs", rollingInterval: RollingInterval.Day)
                   .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            try
            {
                Log.Information("Starting host...");
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    try
                    {
                        var context = services.GetRequiredService<ApplicationDbContext>();

                        await context.Database.MigrateAsync();

                        await ApplicationDbContextSeed.SeedDatabase(context);

                    }

                    catch (Exception ex)
                    {

                        Log.Error(ex, "An error occurred while migrating or seeding the database.");

                        throw;
                    }
                }
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
