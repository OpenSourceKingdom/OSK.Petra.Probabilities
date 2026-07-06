namespace OSK.Petra.Probabilities.Ports;

/// <summary>
/// Provides random number generation for probabilities
/// </summary>
public interface IRandomNumberGenerator
{
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
