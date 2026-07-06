using OSK.Petra.Probabilities.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Petra.Probabilities.RNG;

/// <summary>
/// A random number generator that uses a settings for its generation
/// </summary>
/// <typeparam name="TSettings">The settings the generator uses to create output</typeparam>
public abstract class RandomNumberGenerator<TSettings>: IRandomNumberGenerator
    where TSettings: RandomNumberGeneratorSettings
{
    #region Variables

    private TSettings _settings;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates the random number generator with a given settings
    /// </summary>
    /// <param name="settings">The settings the generator uses to create random output</param>
    public RandomNumberGenerator(TSettings settings)
    {
        settings.Validate();
        _settings = settings;
    }

    #endregion

    #region Api

    /// <summary>
    /// The settings the generator uses to create random output
    /// </summary>
    public TSettings Settings 
    {
        get => _settings;
        set
        {
            value.Validate();
            _settings = value;
        }
    }

    #endregion

    #region IRandomNumberGenerator

    /// <inheritdoc/>
    public abstract int Next(int minValue, int maxValue);

    /// <inheritdoc/>
    public abstract double NextDouble();

    #endregion
}
