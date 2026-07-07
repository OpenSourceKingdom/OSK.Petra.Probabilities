using OSK.Petra.Probabilities.Models;
using OSK.Petra.Probabilities.Ports;

namespace OSK.Petra.Probabilities.Internal.Services;

internal class ProbabilityRoller(IRandomNumberGenerator numberGenerator) : IProbabilityRoller
{
    #region IProbabilityRoller

    public ProbabilityState GetState()
        => new()
        {
            NumberGeneratorState = numberGenerator.GetState()
        };

    public bool RollCheck(double minChance, double maxChance, double probabilityRequired)
        => numberGenerator.NextDouble(minChance, maxChance) >= probabilityRequired;

    public int Roll(int min, int max)
        => numberGenerator.NextInteger(min, max);

    public double Roll(double min, double max)
        => numberGenerator.NextDouble(min, max);

    #endregion
}
