using OSK.Petra.Probabilities.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Petra.Probabilities;

public static class ProbabilityRollerExtensions
{
    public static int RollDie(this IProbabilityRoller roller, int sides)
        => roller.Roll(sides, sides + 1);
}
