using System.Reflection;
using Business.Common.Behaviours;
using Business.Infrastructure.Services;
using Business.Infrastructure.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class DependencyInjection
{
    public static void AddBusinessDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(_ => { }, Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        services.AddScoped<ITokenService, TokenService>();
    }
}