using OSK.Petra.Probabilities.Ports;

namespace OSK.Petra.Probabilities.RNG.Seeded;

/// <summary>
/// The iteration mode to utilize when attempting to reload the <see cref="SeededRandomNumberGenerator"/> to a previous state
/// </summary>
public enum IterationMode
{
    /// <summary>
    /// Use the <see cref="IRandomNumberGenerator.NextInteger(int, int)"/> as the primary loading mechanism
    /// </summary>
    Integer,

    /// <summary>
    /// Use the <see cref="IRandomNumberGenerator.NextDouble(double, double)"/> as the primary loading mechanism
    /// </summary>
    Double
}
