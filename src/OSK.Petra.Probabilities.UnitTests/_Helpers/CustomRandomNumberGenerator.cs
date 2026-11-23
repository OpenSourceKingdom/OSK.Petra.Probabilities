using OSK.Petra.Probabilities.Models;
using OSK.Petra.Probabilities.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Petra.Probabilities.UnitTests._Helpers;

public sealed class CustomRandomNumberGenerator : IRandomNumberGenerator
{
    private int _value;

    public CustomRandomNumberGenerator(CustomRandomState state)
    {
        _value = state.Value;
    }

    public IRandomNumberGenerator Clone()
        => new CustomRandomNumberGenerator(new CustomRandomState { Value = _value });

    public RandomNumberGeneratorState GetState()
        => new CustomRandomState { Value = _value };

    public double NextDouble(double min, double max)
        => min;

    public int NextInteger(int min, int max)
        => _value++;
}
