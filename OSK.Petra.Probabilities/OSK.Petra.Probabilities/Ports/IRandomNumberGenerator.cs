using OSK.Hexagonal.MetaData;
using System;

namespace OSK.Petra.Probabilities.Ports;

/// <summary>
/// Provides random number generation for probabilities
/// </summary>
public interface IRandomNumberGenerator
{
    /// <summary>
    /// Gets a random value between 0.0 and 1.0
    /// </summary>
    /// <returns>The value</returns>
    double NextDouble();

    /// <summary>
    /// Get a random value between min and max
    /// </summary>
    /// <param name="minValue">The min value</param>
    /// <param name="maxValue">The max value</param>
    /// <returns>A value between min/max inclusive</returns>
    int Next(int minValue, int maxValue);
}
