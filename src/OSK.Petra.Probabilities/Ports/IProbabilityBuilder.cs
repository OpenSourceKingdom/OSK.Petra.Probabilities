using OSK.Hexagonal.MetaData;
using System;

namespace OSK.Petra.Probabilities.Ports;

/// <summary>
/// A builder that configures and creates <see cref="IProbabilityRoller"/> for a DI container
/// </summary>
[HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
public interface IProbabilityBuilder
{
    /// <summary>
    /// Use the specified number generator for the rollers by adding it to the related DI container
    /// </summary>
    /// <typeparam name="TGenerator">The generator type</typeparam>
    /// <returns>The builder for chaining</returns>
    IProbabilityBuilder UseRandomNumberGenerator<TGenerator>()
        where TGenerator: class, IRandomNumberGenerator;

    /// <summary>
    /// Use the specified number generator for the rollers by using a factory method
    /// </summary>
    /// <typeparam name="TGenerator">The generator type</typeparam>
    /// <param name="factory">The factory to create the generator with</param>
    /// <returns>The builder for chaining</returns>
    IProbabilityBuilder UseRandomNumberGenerator<TGenerator>(Func<IServiceProvider, TGenerator> factory)
        where TGenerator: class, IRandomNumberGenerator;
}
