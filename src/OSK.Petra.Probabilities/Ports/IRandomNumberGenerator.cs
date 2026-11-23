using OSK.Hexagonal.MetaData;
using OSK.Petra.Probabilities.Models;

namespace OSK.Petra.Probabilities.Ports;

/// <summary>
/// Provides random number generation for probabilities
/// </summary>
[HexagonalIntegration(HexagonalIntegrationType.LibraryProvided, HexagonalIntegrationType.ConsumerOptional, HexagonalIntegrationType.IntegrationOptional)]
public interface IRandomNumberGenerator
{
    /// <summary>
    /// Creates a generator using identical state as the current roller, but separated so rolls don't impact the original
    /// </summary>
    /// <returns>The new generator based on similar state</returns>
    IRandomNumberGenerator Clone();

    /// <summary>
    /// Get the current state for the generator
    /// </summary>
    /// <returns>The current state data</returns>
    RandomNumberGeneratorState GetState();

    /// <summary>
    /// Gets a random decimal value between min and max
    /// </summary>
    /// <returns>The value</returns>
    double NextDouble(double min, double max);

    /// <summary>
    /// Get a random integer value between min and max
    /// </summary>
    /// <param name="min">The min value</param>
    /// <param name="max">The max value</param>
    /// <returns>A value between min/max inclusive</returns>
    int NextInteger(int min, int max);
}
