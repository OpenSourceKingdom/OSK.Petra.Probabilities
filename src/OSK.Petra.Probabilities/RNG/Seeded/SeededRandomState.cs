using OSK.Petra.Probabilities.Models;

namespace OSK.Petra.Probabilities.RNG.Seeded;

/// <summary>
/// State for the pseudo random number generator
/// </summary>
public class SeededRandomState : RandomNumberGeneratorState
{
    /// <summary>
    /// The value that started the generator
    /// </summary>
    public int Seed { get; init; }

    /// <summary>
    /// Stored state to load for the current RNG iteration
    /// </summary>
    public IterationState? IterationState { get; init; }
}
