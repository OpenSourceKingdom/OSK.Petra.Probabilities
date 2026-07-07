using OSK.Petra.Probabilities.Models;

namespace OSK.Petra.Probabilities.RNG.PCG32;

/// <summary>
/// State the PCG32 number generator uses
/// </summary>
public class PCG32RandomState: RandomNumberGeneratorState
{
    /// <summary>
    /// The current state for the generator sequence
    /// </summary>
    public ulong StateValue { get; set; }
}
