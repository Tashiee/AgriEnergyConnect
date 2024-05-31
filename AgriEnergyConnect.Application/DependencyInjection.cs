﻿using AgriEnergyConnect.Application.Abstractions.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AgriEnergyConnect.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            
        });
        
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        return services;
    }
} 