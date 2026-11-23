namespace OSK.Petra.Probabilities.RNG.Seeded;

/// <summary>
/// State used to help iterate and load a seeded RNG to a 'close-to' approximate of a previous state
/// </summary>
public class IterationState
{
    /// <summary>
    /// The mode that is used to iterate the internal seeded RNG state. This should reflect the most frequent usage of the RNG to help get the closest match of internal state
    /// </summary>
    public IterationMode IterationMode { get; init; }

    /// <summary>
    /// The iteration number for the RNG generation.
    /// </summary>
    /// <remarks>
    /// 💡Notes:
    /// <list type="bullet">
    /// <item>Setting an iteration state may require the underlying Seeded PRNG to iterate when constructed or cloned. Be aware that there could be a performance hit depending on the iteration count when cloning</item>
    /// <item>Iterations is only part of a required set of data to determine exact state for a PRNG that utilizes only a seed. The problem is with how samply works when calling different methods for random on the same generator. Doubles, Ints, Byte Arrays all may utilize different amount of sampling of random state, thus unless order of calls is guaranteed and replicated an exact state can not be reproduced. You will only get a 'close-to' state, if you have called the generator with different sequences of random generation and a history of calls is not provided. Due to the potential unwieldy nature of such state loading, we will assume a simple iteration requirement using the <see cref="IterationMode"/> as the only call used. Please set the mode to the usage most frequently used by your generator to get the 'closest' generator after a clone or state load</item>
    /// </list>
    /// </remarks>
    public long Iteration { get; init; }
}
