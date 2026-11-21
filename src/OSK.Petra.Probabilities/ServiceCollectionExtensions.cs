using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OSK.Petra.Probabilities.Internal.Services;
using OSK.Petra.Probabilities.Ports;
using System;

namespace OSK.Petra.Probabilities;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds random probability services to the container with default probability configuration.
        /// </summary>
        /// <returns>The services for chaining.</returns>
        public IServiceCollection AddProbabilities()
            => services.AddProbabilities(_ => { });

        /// <summary>
        /// Adds random probability services to the container.
        /// </summary>
        /// <param name="configurator">The configurator for setting up probability-related services.</param>
        /// <returns>The services for chaining.</returns>
        public IServiceCollection AddProbabilities(Action<IProbabilityBuilder> configurator)
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
}
