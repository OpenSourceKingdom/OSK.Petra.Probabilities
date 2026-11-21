using OSK.Petra.Probabilities.Ports;
using OSK.Petra.Probabilities.RNG.Seeded;

namespace OSK.Petra.Probabilities;

public static class ProbabilityBuilderExtensions
{
    extension(IProbabilityBuilder builder)
    {
        #region Seeded

        /// <summary>
        /// Uses a seeded probability generator with a default seed.
        /// </summary>
        /// <returns>The builder for chaining.</returns>
        public IProbabilityBuilder UseSeededProbability()
            => builder.UseRandomNumberGenerator<SeededRandomNumberGenerator>();

        /// <summary>
        /// Uses a seeded probability generator with a provided seed.
        /// </summary>
        /// <param name="seed">The seed to start the generator with.</param>
        /// <returns>The builder for chaining.</returns>
        public IProbabilityBuilder UseSeededProbability(int seed)
            => builder.UseRandomNumberGenerator(_ => new SeededRandomNumberGenerator(seed));

        #endregion

        #region PCG32

        /// <summary>
        /// Uses a PCG32 probability generator with a default state.
        /// </summary>
        /// <returns>The builder for chaining.</returns>
        public IProbabilityBuilder UsePCG32()
            => builder.UseRandomNumberGenerator<PCG32RandomNumberGenerator>();

        /// <summary>
        /// Uses a PCG32 probability generator with a provided state.
        /// </summary>
        /// <param name="state">The state to start the generator with.</param>
        /// <returns>The builder for chaining.</returns>
        public IProbabilityBuilder UsePCG32(ulong state)
            => builder.UseRandomNumberGenerator(_ => new PCG32RandomNumberGenerator(state));

        #endregion
    }
}