using OSK.Petra.Probabilities.Models;
using OSK.Petra.Probabilities.Ports;
using System;

namespace OSK.Petra.Probabilities.RNG.Seeded;

/// <summary>
/// Creates a <see cref="IRandomNumberGenerator"/> that utilizes .NET Psuedo random
/// </summary>
public class SeededRandomNumberGenerator : IRandomNumberGenerator
{
    #region Variables

    private readonly IterationMode? _iterationMode;
    private long _iteration;
    private readonly int _seed;
 
    private readonly Random _randomGenerator;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a pseudo random generator using a randomly created seed
    /// </summary>
    /// <param name="options">Sets options to internal iteration tracking for state load</param>
    public SeededRandomNumberGenerator(IterationOptions? options = null)
        : this(SeedHelpers.RandomSeed32())
    {
    }

    /// <summary>
    /// Creates a pseudo random generator using a known seed
    /// </summary>
    /// <param name="seed">The seed to set the generator with</param>
    /// <param name="options">Sets options to internal iteration tracking for state load</param>
    public SeededRandomNumberGenerator(int seed, IterationOptions? options = null)
    {
        _seed = seed;
        _randomGenerator = new(seed);

        _iterationMode = options?.IterationMode;
    }

    /// <summary>
    /// loads the RNG with provided state
    /// </summary>
    /// <param name="state">The state to load</param>
    /// <exception cref="ArgumentNullException">If state is null</exception>
    public SeededRandomNumberGenerator(SeededRandomState state)
    {
        _seed = state?.Seed ?? throw new ArgumentNullException(nameof(state));
        _randomGenerator = new(state.Seed);

        if (state.IterationState is not null)
        {
            _iterationMode = state.IterationState.IterationMode;
            _iteration = state.IterationState.Iteration;

            for (int iteration = 0; iteration < state.IterationState.Iteration; iteration++)
            {
                switch (state.IterationState.IterationMode)
                {
                    case IterationMode.Integer:
                        _randomGenerator.Next();
                        break;
                    case IterationMode.Double:
                        _randomGenerator.NextDouble();
                        break;
                }
            }
        }
    }

    #endregion

    #region IRandomNumberGenerator

    /// <inheritdoc/>
    public IRandomNumberGenerator Clone()
        => new SeededRandomNumberGenerator(_seed);

    /// <inheritdoc/>
    public RandomNumberGeneratorState GetState()
        => new SeededRandomState()
        {
            Seed = _seed,
            IterationState = _iterationMode is null
                ? null
                : new()
                {
                    IterationMode = _iterationMode.Value,
                    Iteration = _iteration
                }
        };

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

        if (_iterationMode is not null)
        {
            _iteration++;
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

        if (_iterationMode is not null)
        {
            _iteration++;
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
