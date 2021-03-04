using Docker.DotNet;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Redpier.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton<IDockerClient>(
                    new DockerClientConfiguration(
                        new Uri(configuration.GetValue<string>("DockerUri"))
                        ).CreateClient());

            //services.AddScoped<IDockerClient, DockerClient>();

            return services;
        }
    }
}
