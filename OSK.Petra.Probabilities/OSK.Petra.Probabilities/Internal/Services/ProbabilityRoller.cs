using OSK.Petra.Probabilities.Ports;
using OSK.Petra.Probabilities.RNG;
using OSK.Petra.Probabilities.RNG.Pseudo;
using System;

namespace OSK.Petra.Probabilities.Internal.Services;

internal class ProbabilityRoller(IRandomNumberGenerator numberGenerator) : IProbabilityRoller
{
    #region IProbabilityRoller

    public bool Chance(double probability)
        => numberGenerator.NextDouble() <= probability;

    public int Roll(int min, int max)
        => numberGenerator.Next(min, max);

    #endregion
}
