using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Petra.Probabilities.RNG;

/// <summary>
/// A settings object that is used to provide required state and other data to assist with random number generation
/// </summary>
public abstract class RandomNumberGeneratorSettings
{
    /// <summary>
    /// Validates the settings object is correct to what is expected.
    /// </summary>
    /// /// <remarks>
    /// 💡Notes:
    /// <list type="bullet">
    /// <item>This is expected to throw if validation fails</item>
    /// </list>
    /// </remarks>
    public virtual void Validate()
    {
    }
}
