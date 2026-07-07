namespace OSK.Petra.Probabilities.Models;

/// <summary>
/// The state for a probability roller
/// </summary>
public class ProbabilityState
{
    /// <summary>
    /// The state for the internal random number generator the roller was utilizing
    /// </summary>
    public required RandomNumberGeneratorState NumberGeneratorState { get; set; }
}
