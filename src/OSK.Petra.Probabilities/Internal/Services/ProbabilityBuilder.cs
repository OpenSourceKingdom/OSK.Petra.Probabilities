using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OSK.Petra.Probabilities.Ports;
using OSK.Petra.Probabilities.RNG.Seeded;
using System;

namespace OSK.Petra.Probabilities.Internal.Services;

internal class ProbabilityBuilder(IServiceCollection services) : IProbabilityBuilder
{
    #region IProbabilityBuilder

    public IProbabilityBuilder UseRandomNumberGenerator<TGenerator>() 
        where TGenerator : class, IRandomNumberGenerator
    {
        services.TryAddTransient<IRandomNumberGenerator, TGenerator>();
        return this;
    }

    public IProbabilityBuilder UseRandomNumberGenerator<TGenerator>(Func<IServiceProvider, TGenerator> factory)
        where TGenerator : class, IRandomNumberGenerator
    {
        if (factory is null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        services.TryAddTransient<IRandomNumberGenerator>(factory);
        return this;
    }

    #endregion

    #region Api

    internal IProbabilityRoller Build(IServiceProvider serviceProvider)
    {
        var randomNumberGenerator = serviceProvider.GetService<IRandomNumberGenerator>() ?? new SeededRandomNumberGenerator();
        return new ProbabilityRoller(randomNumberGenerator);
    }

    #endregion
}
