using OSK.Hexagonal.MetaData;
using OSK.Petra.Probabilities.Models;

namespace OSK.Petra.Probabilities.Ports;

/// <summary>
/// A probability roller acts as a source of random for a game for either dice or other style rolls or for chance actions
/// </summary>
[HexagonalIntegration(HexagonalIntegrationType.LibraryProvided, HexagonalIntegrationType.ConsumerPointOfEntry)]
public interface IProbabilityRoller
{
    /// <summary>
    /// The probability state that can be used to restore the probability roller
    /// </summary>
    ProbabilityState GetState();

    /// <summary>
    /// Randomly roll between the min and max values, inclusive
    /// </summary>
    /// <param name="min">The min value</param>
    /// <param name="max">The max value</param>
    /// <returns>The roll value</returns>
    int Roll(int min, int max);

    /// <summary>
    /// Randomly roll between the min and max values, max exclusive
    /// </summary>
    /// <param name="min">The min value</param>
    /// <param name="max">The max value</param>
    /// <returns>The roll value</returns>
    double Roll(double min, double max);
}
