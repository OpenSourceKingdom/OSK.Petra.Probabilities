using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using OSK.Petra.Probabilities.Internal.Services;
using OSK.Petra.Probabilities.Ports;

namespace OSK.Petra.Probabilities;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds random probability services to the container
    /// </summary>
    /// <param name="services">The service container to add the DI to</param>
    /// <returns>The services for chaining</returns>
    public static IServiceCollection AddProbabilities(this IServiceCollection services)
    {
        services.TryAddTransient<IProbabilityRoller, ProbabilityRoller>();

        return services;
    }
}
