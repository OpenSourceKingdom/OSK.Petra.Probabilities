using OSK.Petra.Probabilities.Ports;
using System;
using System.Security.Cryptography;

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
        : this(RandomHelpers.CreateRandomSeed32())
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
    public double NextDouble()
        => _randomGenerator.NextDouble();

    /// <inheritdoc/>
    public int Next(int minValue, int maxValue)
    {
        if (minValue > maxValue)
        {
            throw new InvalidOperationException($"Unable to get a value between min and max becasue the min value '{minValue}' can not be greater than the max '{maxValue}.");
        }
        if (minValue == maxValue)
        {
            return minValue;
        }
        if (maxValue is int.MaxValue)
        {
            return _randomGenerator.Next(minValue, maxValue);
        }

        return _randomGenerator.Next(minValue, maxValue + 1);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
    }

    #endregion
}
