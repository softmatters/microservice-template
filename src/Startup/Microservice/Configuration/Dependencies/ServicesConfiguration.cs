using Microservice.Application;
using Microservice.Application.Authentication.Helpers;
using Microservice.Application.Contracts.Repositories;
using Microservice.Application.Contracts.Services;
using Microservice.Infrastructure.Repositories;
using Microservice.Services;

namespace Microservice.Configuration.Dependencies;

/// <summary>
///  User Defined Services Configuration
/// </summary>
public static class ServicesConfiguration
{
    /// <summary>
    /// Adds services to the IoC container
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // example of configuring the IoC container to inject the dependencies

        services.AddSingleton<ITokenHelper, TokenHelper>();

        services.AddTransient<IService, Service>();
        services.AddTransient<IRepository, Repository>();
        services.AddMediatR(option => option.RegisterServicesFromAssemblyContaining<IApplication>());

        return services;
    }
}