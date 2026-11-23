using OSK.Petra.Probabilities.Internal.Services;
using OSK.Petra.Probabilities.Models;
using OSK.Petra.Probabilities.Ports;
using OSK.Petra.Probabilities.RNG.PCG32;
using OSK.Petra.Probabilities.RNG.Seeded;
using System;
using System.Collections.Generic;

namespace OSK.Petra.Probabilities;

/// <summary>
/// A factory style class to help with creating probability rollers outside of DI
/// </summary>
public static class Probability
{
    #region Variables

    private static readonly Dictionary<Type, Type> _stateRngTypeLookup = new()
    {
        { typeof(SeededRandomState), typeof(SeededRandomNumberGenerator) },
        { typeof(PCG32RandomState), typeof(PCG32RandomNumberGenerator) }
    };

    #endregion

    #region RNG Registration

    /// <summary>
    /// Registers a typed generator and state so that a customer RNG roller can be generated using the <see cref="CreateRoller(ProbabilityState)"/> call.
    /// </summary>
    /// <typeparam name="TGenerator">The generator type to use</typeparam>
    /// <typeparam name="TState">The state the generator uses</typeparam>
    public static void RegisterNumberGeneratorState<TGenerator, TState>()
        where TGenerator: IRandomNumberGenerator
        where TState: RandomNumberGeneratorState
    {
        _stateRngTypeLookup[typeof(TState)] = typeof(TGenerator);
    }

    #endregion

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
    /// Creates a roller from a probability state directly. If using a custom <see cref="RandomNumberGeneratorState"/>, please ensure the state and generator type matches are registered using 
    /// <see cref="RegisterNumberGeneratorState{TGenerator, TState}"/>
    /// </summary>
    /// <param name="state">The state to load the probability roller with</param>
    /// <returns>The roller with provided state</returns>
    /// <exception cref="ArgumentNullException">If the state is null</exception>
    /// <exception cref="InvalidOperationException">If the underlying RNG state is not defined</exception>
    public static IProbabilityRoller CreateRoller(ProbabilityState state)
    {
        if (state is null)
        {
            throw new ArgumentNullException(nameof(state));
        }
        if (state.NumberGeneratorState is null)
        {
            throw new InvalidOperationException("Number generator can not be null.");
        }

        if (!_stateRngTypeLookup.TryGetValue(state.NumberGeneratorState.GetType(), out var generatorType)) 
        {
            throw new InvalidOperationException($"The number generator state type '{state.NumberGeneratorState.GetType().FullName}' is not registered and can not be used. Please consider registering it, or use {nameof(CreateRoller)} with a generator directly");
        }

        return CreateRoller((IRandomNumberGenerator)Activator.CreateInstance(generatorType, [state.NumberGeneratorState]));
    }

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
