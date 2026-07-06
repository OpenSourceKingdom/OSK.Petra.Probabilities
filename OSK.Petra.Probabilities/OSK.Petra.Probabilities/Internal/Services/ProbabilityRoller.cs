using OSK.Petra.Probabilities.Ports;
using OSK.Petra.Probabilities.RNG;
using OSK.Petra.Probabilities.RNG.Pseudo;
using System;

namespace OSK.Petra.Probabilities.Internal.Services;

internal class ProbabilityRoller : IProbabilityRoller
{
    #region Variables

    private IRandomNumberGenerator _numberGenerator = new PseudoRandomNumberGenerator();

    #endregion

    #region IProbabilityRoller

    public bool Chance(double probability)
        => _numberGenerator.NextDouble() <= probability;

    public int Roll(int min, int max)
        => _numberGenerator.Next(min, max);

    public void Configure(IRandomNumberGenerator numberGenerator)
    {
        if (numberGenerator is null)
        {
            throw new ArgumentNullException(nameof(numberGenerator));
        }

        _numberGenerator = numberGenerator;
    }

    #endregion
}
