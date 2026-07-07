using OSK.Petra.Probabilities.Ports;

namespace OSK.Petra.Probabilities;

public static class RandomNumberGeneratorExtensions
{
    /// <summary>
    /// Get a random double between 0.0 and 1.0
    /// </summary>
    /// <param name="numberGenerator">The number generator to use</param>
    /// <returns>A random value</returns>
    public static double NextDouble(this IRandomNumberGenerator numberGenerator)
        => numberGenerator.NextDouble(0.0, 1.0);
}
