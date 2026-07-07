using OSK.Petra.Probabilities.Ports;
using OSK.Petra.Probabilities.RNG.Pseudo;

namespace OSK.Petra.Probabilities;

public static class ProbabilityBuilderExtensions
{
    #region Seeded

    /// <summary>
    /// Uses a seeded proobability generator, with a default seed
    /// </summary>
    /// <param name="builder">The builder to add the generator to</param>
    /// <returns>The builder for chaining</returns>
    public static IProbabilityBuilder UseSeededProbability(this IProbabilityBuilder builder)
        => builder.UseRandomNumberGenerator<SeededRandomNumberGenerator>();

    /// <summary>
    /// Uses a seeded probability generator, with a provided seed
    /// </summary>
    /// <param name="builder">The builder to add the generator to</param>
    /// <param name="seed">The seed to start the generator with</param>
    /// <returns>The builder for chaining</returns>
    public static IProbabilityBuilder UseSeededProbability(this IProbabilityBuilder builder, int seed)
        => builder.UseRandomNumberGenerator(_ => new SeededRandomNumberGenerator(seed));

    #endregion

    #region PCG32

    /// <summary>
    /// Uses a PCG32 probability generator, with a default state
    /// </summary>    
    /// <param name="builder">The builder to add the generator to</param>
    /// <returns>The builder for chaining</returns>
    public static IProbabilityBuilder UsePCG32(this IProbabilityBuilder builder)
        => builder.UseRandomNumberGenerator<PCG32RandomNumberGenerator>();

    /// <summary>
    /// Uses a PCG32 probability generator, with a provided state
    /// </summary>
    /// <param name="builder">The builder to add the generator to</param>
    /// <param name="state">The state to start the generator with</param>
    /// <returns>The builder for chaining</returns>
    public static IProbabilityBuilder UsePCG32(this IProbabilityBuilder builder, ulong state)
        => builder.UseRandomNumberGenerator(_ => new PCG32RandomNumberGenerator(state));

    #endregion
}
