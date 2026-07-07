using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OSK.Petra.Probabilities.Internal.Services;
using OSK.Petra.Probabilities.Ports;
using System;

namespace OSK.Petra.Probabilities;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds random probability services to the container
    /// </summary>
    /// <param name="services">The service container to add the DI to</param>
    /// <returns>The services for chaining</returns>
    public static IServiceCollection AddProbabilities(this IServiceCollection services, Action<IProbabilityBuilder> configurator)
    {
        if (configurator is null)
        {
            throw new ArgumentNullException(nameof(configurator));
        }

        var builder = new ProbabilityBuilder(services);
        configurator(builder);
        services.TryAddSingleton(builder.Build);

        return services;
    }
}
