using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Petra.Probabilities.RNG.PCG32;

public class PCG32Settings: RandomNumberGeneratorSettings
{
    public ulong State { get; set; }

    public ulong Increment { get; set; }
}
