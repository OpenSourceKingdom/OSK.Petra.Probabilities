using OSK.Petra.Probabilities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Petra.Probabilities.UnitTests._Helpers;

public sealed class CustomRandomState : RandomNumberGeneratorState
{
    public int Value { get; set; }
}
