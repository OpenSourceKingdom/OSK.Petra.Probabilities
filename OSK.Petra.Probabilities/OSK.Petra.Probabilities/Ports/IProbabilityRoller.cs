using OSK.Hexagonal.MetaData;

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
    /// <param name="minChance">The min chance value for success</param>
    /// <param name="maxChance">The max chance value for success</param>
    /// <param name="probabilityRequired">The probabilistic chance that a chance should succeed</param>
    /// <returns>Whether the chance succeeded</returns>
    bool RollCheck(double minChance, double maxChance, double probabilityRequired);

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
