using OSK.Petra.Probabilities.Internal.Services;
using OSK.Petra.Probabilities.Ports;
using OSK.Petra.Probabilities.RNG.Seeded;
using System;

namespace OSK.Petra.Probabilities;

/// <summary>
/// A factory style class to help with creating probability rollers outside of DI
/// </summary>
public static class Probability
{
    #region Seeded

    /// <summary>
    /// Creates a probability roller with a seeded generator, using a default seed
    /// </summary>
    /// <returns>The roller</returns>
    public static IProbabilityRoller CreateSeededRandomRoller()
        => CreateRoller(new SeededRandomNumberGenerator());

    /// <summary>
    /// Creates a probability roller with a seeded generator, using a default seed
    /// </summary>
    /// <param name="seed">The seed to start the generator with</param>
    /// <returns>The roller</returns>
    public static IProbabilityRoller CreateSeededRandomRoller(int seed)
        => CreateRoller(new SeededRandomNumberGenerator(seed));

    #endregion

    #region PCG32

    /// <summary>
    /// Creates a probability roller with a PCG32 generator, using a default state
    /// </summary>
    /// <returns>The roller</returns>
    public static IProbabilityRoller CreatePCG32()
        => CreateRoller(new PCG32RandomNumberGenerator());

    /// <summary>
    /// Creates a probability roller with a PCG32 generator, using a default state
    /// </summary>
    /// <param name="state">The state to start the generator with</param>
    /// <returns>The roller</returns>
    public static IProbabilityRoller CreatePCG32(ulong state)
        => CreateRoller(new PCG32RandomNumberGenerator(state));

    #endregion

    #region Generic

    /// <summary>
    /// Creates a probability roller with a given number generator
    /// </summary>
    /// <param name="randomNumberGenerator">The number generator to use with the probability roller</param>
    /// <returns>The roller</returns>
    /// <exception cref="ArgumentNullException">Thrown if the number generator is null</exception>
    public static IProbabilityRoller CreateRoller(IRandomNumberGenerator randomNumberGenerator)
    {
        if (randomNumberGenerator is null)
        {
            throw new ArgumentNullException(nameof(randomNumberGenerator));
        }

        return new ProbabilityRoller(randomNumberGenerator);
    }

    #endregion
}
