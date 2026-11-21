using OSK.Petra.Probabilities.Models;
using OSK.Petra.Probabilities.Ports;

namespace OSK.Petra.Probabilities;

public static class RandomNumberGeneratorExtensions
{
    extension(IRandomNumberGenerator numberGenerator)
    {
        /// <summary>
        /// Gets a random double between 0.0 and 1.0.
        /// </summary>
        /// <returns>A random value.</returns>
        public double NextDouble()
            => numberGenerator.NextDouble(0.0, 1.0);

        /// <summary>
        /// Gets the state for the random number generator, cast to the specified type.
        /// </summary>
        /// <typeparam name="TState">The type of state.</typeparam>
        /// <returns>The state, if the cast is successful.</returns>
        public TState GetState<TState>()
            where TState : RandomNumberGeneratorState
            => (TState)numberGenerator.GetState();
    }
}
