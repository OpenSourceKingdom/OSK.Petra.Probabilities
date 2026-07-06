using OSK.Petra.Probabilities.Ports;
using System;

namespace OSK.Petra.Probabilities.RNG.Pseudo;

/// <summary>
/// Creates a <see cref="IRandomNumberGenerator"/> that utilizes .NET Psuedo random
/// </summary>
public class PseudoRandomNumberGenerator : IRandomNumberGenerator
{
    #region Variables

    private readonly Random _randomGenerator;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a pseudo random generator using a randomly created seed
    /// </summary>
    public PseudoRandomNumberGenerator()
        : this(SeedHelpers.RandomSeed32())
    {
    }

    /// <summary>
    /// Creates a pseudo random generator using a known seed
    /// </summary>
    /// <param name="seed">The seed to set the generator with</param>
    public PseudoRandomNumberGenerator(int seed)
    {
        Seed = seed;
        _randomGenerator = new(seed);
    }

    #endregion

    #region Api

    /// <summary>
    /// The seed that started the generator
    /// </summary>
    public int Seed { get; }

    #endregion

    #region IRandomNumberGenerator

    /// <inheritdoc/>
    public double NextDouble(double min, double max)
    {
        if (min > max)
        {
            throw new ArgumentOutOfRangeException(nameof(min), $"Unable to get an integer value between min and max becasue the min value '{min}' can not be greater than the max '{max}.");
        }
        if (min == max)
        {
            return min;
        }

        return min + (_randomGenerator.NextDouble() * (max - min));
    }

    /// <inheritdoc/>
    public int NextInteger(int min, int max)
    {
        if (min > max)
        {
            throw new InvalidOperationException($"Unable to get an integer value between min and max becasue the min value '{min}' can not be greater than the max '{max}.");
        }
        if (min == max)
        {
            return min;
        }
        if (max is int.MaxValue)
        {
            return _randomGenerator.Next(min, max);
        }

        return _randomGenerator.Next(min, max + 1);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
    }

    #endregion
}
