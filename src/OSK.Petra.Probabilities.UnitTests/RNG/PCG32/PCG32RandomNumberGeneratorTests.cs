using OSK.Petra.Probabilities.RNG.PCG32;

namespace OSK.Petra.Probabilities.UnitTests.RNG.PCG32;

public class PCG32RandomNumberGeneratorTests: DeterministicRandomNumberGeneratorTests<PCG32RandomNumberGenerator>
{

    #region DeterministicRandomNumberGeneratorOverrides

    protected override PCG32RandomNumberGenerator CreateDefaultGenerator()
        => new(42);

    #endregion

    #region Constructor Tests

    [Fact]
    public void Constructor_WithoutState_InitializesGenerator()
    {
        // Arrange & Act
        PCG32RandomNumberGenerator generator = new();

        // Assert
        Assert.NotNull(generator);
        Assert.NotEqual(0UL, generator.GetState<PCG32RandomState>().StateValue);
    }

    [Fact]
    public void Constructor_WithState_InitializesGeneratorWithGivenSeed()
    {
        // Arrange
        ulong state = 12345UL;

        // Act
        PCG32RandomNumberGenerator generator = new(state);

        // Assert
        Assert.NotNull(generator);
        Assert.Equal(state, generator.GetState<PCG32RandomState>().StateValue);
    }

    [Fact]
    public void Constructor_WithCapturedState_ContinuesSequence()
    {
        // Arrange
        var generator = new PCG32RandomNumberGenerator(12345UL);
        for (int i = 0; i < 5; i++)
        {
            generator.NextDouble(0.0, 1.0);
        }

        var state = generator.GetState<PCG32RandomState>();
        var restoredGenerator = new PCG32RandomNumberGenerator(state);

        // Act
        var expectedValues = new double[5];
        var restoredValues = new double[5];
        for (int i = 0; i < expectedValues.Length; i++)
        {
            expectedValues[i] = generator.NextDouble(-10.0, 10.0);
            restoredValues[i] = restoredGenerator.NextDouble(-10.0, 10.0);
        }

        // Assert
        Assert.Equal(expectedValues, restoredValues);
    }

    [Fact]
    public void Clone_CreatesIndependentGeneratorAtCurrentState()
    {
        // Arrange
        var generator = new PCG32RandomNumberGenerator(12345UL);
        generator.NextInteger(0, 100);

        // Act
        var clone = generator.Clone();

        // Assert
        Assert.Equal(generator.NextInteger(0, 100), clone.NextInteger(0, 100));
        Assert.Equal(generator.NextDouble(0.0, 1.0), clone.NextDouble(0.0, 1.0));
    }

    #endregion
}
