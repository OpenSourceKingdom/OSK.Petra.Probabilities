namespace OSK.Petra.Probabilities.RNG.Seeded;

/// <summary>
/// A set of options that can be used with <see cref="SeededRandomState"/> to better load a seeded RNG
/// </summary>
public class IterationOptions
{
    /// <summary>
    /// The iteration mode to use when performing seed iterations when loading a state
    /// </summary>
    public IterationMode IterationMode { get; init; }
}
