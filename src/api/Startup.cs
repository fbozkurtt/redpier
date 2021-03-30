using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Redpier.Application;
using Redpier.Application.Common.Interfaces;
using Redpier.Infrastructure;
using Redpier.Web.API.Filters;
using Redpier.Web.API.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics;
using System.Reflection;

namespace Redpier.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            ProductVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }
        public string ProductVersion { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication(Configuration);

            services.AddHttpContextAccessor();

            services.AddInfrastructure(Configuration, Environment);

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSPA",
                                  builder =>
                                  {
                                      builder.WithOrigins("https://localhost:5001")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                                  });
            });

            services.AddControllers(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>());

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ProductVersion, new OpenApiInfo { Title = "Redpier API", Version = ProductVersion });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseMigrationsEndPoint();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseApplicationResponse();

            app.UseCors("AllowSPA");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{ProductVersion}/swagger.json", $"Redpier API {ProductVersion}");
                options.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
