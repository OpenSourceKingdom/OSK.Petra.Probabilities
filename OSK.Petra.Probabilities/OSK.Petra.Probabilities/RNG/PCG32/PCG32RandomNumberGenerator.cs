using OSK.Petra.Probabilities.Ports;
using OSK.Petra.Probabilities.RNG;
using OSK.Petra.Probabilities.RNG.PCG32;
using System;
using System.Runtime;

public class PCG32RandomNumberGenerator : IRandomNumberGenerator
{
    #region Variables

    // pcg32d_13_16's stream
    private const ulong Increment = 11634580027462358144;
    private const ulong Multiplier = 6364136223846793005;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a PCG32 random generator using a randomly created seed state
    /// </summary>
    public PCG32RandomNumberGenerator()
        : this((RandomHelpers.CreateLongRandomSeed64() + Increment) * Multiplier + Increment)
    {
    }

    /// <summary>
    /// Creates a PCG32 random generator using a known seed
    /// </summary>
    /// <param name="state">The state to set the generator with</param>
    public PCG32RandomNumberGenerator(ulong state)
    {
        State = state;
    }

    #endregion

    #region Api

    public ulong State { get; set; }

    #endregion

    #region IRandomNumberGenerator

    /// <inheritdoc/>
    public double NextDouble()
    {
        var (updatedState, value) = NextUInt(State);
        State = updatedState;

        return value / (double)uint.MaxValue;
    }

    /// <inheritdoc/>
    public int Next(int minValue, int maxValue)
    {
        if (minValue > maxValue)
        {
            throw new InvalidOperationException($"Unable to get a value between min and max because the min value '{minValue}' cannot be greater than the max '{maxValue}'.");
        }
        if (minValue == maxValue)
        {
            return minValue;
        }

        var range = (uint)(maxValue - minValue + 1);
        var limit = uint.MaxValue - (uint.MaxValue % range);

        uint result;
        do
        {
            (State, result) = NextUInt(State);
        } while (result >= limit);

        return minValue + (int)(result % range);
    }

    #endregion

    #region Helpers

    private (ulong State, uint RandomValue) NextUInt(ulong currentState)
    {
        // PCG32 algorithm
        var updatedState = currentState * Multiplier + Increment;

        uint xorshifted = (uint)(((currentState >> 18) ^ currentState) >> 27);
        int rot = (int)(currentState >> 59);
        var nextValue = (xorshifted >> rot) | (xorshifted << (32 - rot));

        return new (updatedState, nextValue);
    }

    #endregion
}