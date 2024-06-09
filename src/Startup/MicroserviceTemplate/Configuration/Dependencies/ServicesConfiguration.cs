using MicroserviceTemplate.Application;
using MicroserviceTemplate.Application.Authentication.Helpers;
using MicroserviceTemplate.Application.Contracts.Repositories;
using MicroserviceTemplate.Application.Contracts.Services;
using MicroserviceTemplate.Infrastructure.Repositories;
using MicroserviceTemplate.Services;

namespace MicroserviceTemplate.Configuration.Dependencies;

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