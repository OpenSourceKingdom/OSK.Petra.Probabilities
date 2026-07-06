using OSK.Petra.Probabilities;
using OSK.Petra.Probabilities.Ports;
using System;

/// <summary>
/// A random number generator utilizing the PCG32 Algorithm
/// </summary>
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
        : this(SeedHelpers.RandomSeed64())
    {
    }

    /// <summary>
    /// Creates a PCG32 random generator using a known state
    /// </summary>
    /// <param name="state">The state to set the generator with</param>
    public PCG32RandomNumberGenerator(ulong state)
    {
        State = state;
    }

    #endregion

    #region Api

    /// <summary>
    /// The state for the current number generation.
    /// </summary>
    public ulong State { get; set; }

    #endregion

    #region IRandomNumberGenerator

    /// <inheritdoc/>
    public double NextDouble(double min, double max)
    {
        if (min > max)
        {
            throw new InvalidOperationException($"Unable to get a value between min and max because the min value '{min}' cannot be greater than the max '{max}'.");
        }
        if (min == max)
        {
            return min;
        }

        var (updatedState, value) = NextUInt(State);
        State = updatedState;

        var randomFraction = value / (double)uint.MaxValue;
        return min + (randomFraction * (max - min));
    }

    /// <inheritdoc/>
    public int NextInteger(int min, int max)
    {
        if (min > max)
        {
            throw new InvalidOperationException($"Unable to get a value between min and max because the min value '{min}' cannot be greater than the max '{max}'.");
        }
        if (min == max)
        {
            return min;
        }

        var range = (uint)(max - min + 1);
        var limit = uint.MaxValue - (uint.MaxValue % range);

        uint result;
        do
        {
            (State, result) = NextUInt(State);
        } while (result >= limit);

        return min + (int)(result % range);
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