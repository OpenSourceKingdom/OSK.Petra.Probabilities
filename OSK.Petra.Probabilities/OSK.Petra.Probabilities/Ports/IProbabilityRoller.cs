using OSK.Hexagonal.MetaData;
using System;

namespace OSK.Petra.Probabilities.Ports;

/// <summary>
/// A probability roller acts as a source of random for a game for either dice or other style rolls or for chance actions
/// </summary>
[HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
public interface IProbabilityRoller
{
    /// <summary>
    /// Validates if the chance check succeeded
    /// </summary>
    /// <param name="probability">The probabilistic chance that a chance should succeed</param>
    /// <returns>Whether the chacne succeeded</returns>
    bool Chance(double probability);

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
