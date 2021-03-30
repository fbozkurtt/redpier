using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redpier.Application.Common.Behaviours;
using System.Reflection;

namespace Redpier.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            //services.AddSingleton<IDockerClient>(
            //        new DockerClientConfiguration(
            //            new Uri(configuration.GetValue<string>("DockerUri"))
            //            ).CreateClient());

            return services;
        }
    }
}
