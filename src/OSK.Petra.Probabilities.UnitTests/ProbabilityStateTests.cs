using OSK.Petra.Probabilities.Models;
using OSK.Petra.Probabilities.RNG.PCG32;
using OSK.Petra.Probabilities.RNG.Seeded;
using OSK.Petra.Probabilities.UnitTests._Helpers;

namespace OSK.Petra.Probabilities.UnitTests;

public class ProbabilityStateTests
{
    #region State Restoration

    [Fact]
    public void CreateRoller_WithPCG32State_ContinuesSequence()
    {
        // Arrange
        var generator = new PCG32RandomNumberGenerator(12345UL);
        generator.NextDouble(0.0, 1.0);
        var state = new ProbabilityState { NumberGeneratorState = generator.GetState() };
        var restoredRoller = Probability.CreateRoller(state);

        // Act
        var expected = generator.NextDouble(-10.0, 10.0);
        var actual = restoredRoller.Roll(-10.0, 10.0);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CreateRoller_WithSeededState_ContinuesTrackedSequence()
    {
        // Arrange
        var generator = new SeededRandomNumberGenerator(12345, new IterationOptions { IterationMode = IterationMode.Double });
        generator.NextDouble(0.0, 1.0);
        var state = new ProbabilityState { NumberGeneratorState = generator.GetState() };
        var restoredRoller = Probability.CreateRoller(state);

        // Act
        var expected = generator.NextDouble(-10.0, 10.0);
        var actual = restoredRoller.Roll(-10.0, 10.0);

        // Assert
        Assert.Equal(expected, actual);
    }

    #endregion

    #region Clone

    [Fact]
    public void Clone_CreatesIndependentRollerAtCurrentState()
    {
        // Arrange
        var roller = Probability.CreatePCG32(12345UL);
        roller.Roll(0.0, 1.0);

        // Act
        var clone = roller.Clone();
        var originalFirstRoll = roller.Roll(0, 100);
        var cloneFirstRoll = clone.Roll(0, 100);
        var originalSecondRoll = roller.Roll(0, 100);
        var cloneSecondRoll = clone.Roll(0, 100);

        // Assert
        Assert.Equal(originalFirstRoll, cloneFirstRoll);
        Assert.Equal(originalSecondRoll, cloneSecondRoll);
    }

    #endregion

    #region Custom Generator Registration

    [Fact]
    public void CreateRoller_WithRegisteredCustomState_CreatesCustomGenerator()
    {
        // Arrange
        Probability.RegisterNumberGeneratorState<CustomRandomNumberGenerator, CustomRandomState>();
        var state = new ProbabilityState
        {
            NumberGeneratorState = new CustomRandomState { Value = 7 }
        };

        // Act
        var roller = Probability.CreateRoller(state);

        // Assert
        Assert.Equal(7, roller.Roll(0, 10));
    }

    #endregion

    #region Invalid State

    [Fact]
    public void CreateRoller_WithNullState_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Probability.CreateRoller((ProbabilityState)null!));
    }

    [Fact]
    public void CreateRoller_WithNullGeneratorState_ThrowsInvalidOperationException()
    {
        var state = new ProbabilityState { NumberGeneratorState = null! };

        Assert.Throws<InvalidOperationException>(() => Probability.CreateRoller(state));
    }

    [Fact]
    public void CreateRoller_WithUnregisteredState_ThrowsInvalidOperationException()
    {
        var state = new ProbabilityState { NumberGeneratorState = new UnregisteredRandomState() };

        Assert.Throws<InvalidOperationException>(() => Probability.CreateRoller(state));
    }

    #endregion
}
