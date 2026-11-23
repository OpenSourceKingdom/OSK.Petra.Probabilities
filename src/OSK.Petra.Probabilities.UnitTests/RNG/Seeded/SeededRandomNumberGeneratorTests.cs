using OSK.Petra.Probabilities.RNG.Seeded;

namespace OSK.Petra.Probabilities.UnitTests.RNG.Seeded;

public class SeededRandomNumberGeneratorTests: DeterministicRandomNumberGeneratorTests<SeededRandomNumberGenerator>
{

    #region DeterministicRandomNumberGeneratorOverrides

    protected override SeededRandomNumberGenerator CreateDefaultGenerator()
        => new(42);

    #endregion

    #region Constructor Tests

    [Fact]
    public void Constructor_WithoutSeed_InitializesGenerator()
    {
        // Arrange & Act
        SeededRandomNumberGenerator generator = new();

        // Assert
        Assert.NotNull(generator);
        Assert.NotEqual(0, generator.GetState<SeededRandomState>().Seed);
    }

    [Fact]
    public void Constructor_WithSeed_InitializesGeneratorWithGivenSeed()
    {
        // Arrange
        int seed = 12345;

        // Act
        SeededRandomNumberGenerator generator = new(seed);

        // Assert
        Assert.NotNull(generator);
        Assert.Equal(seed, generator.GetState<SeededRandomState>().Seed);
    }

    [Fact]
    public void Constructor_WithIterationOptions_TracksGeneratedValues()
    {
        // Arrange
        SeededRandomNumberGenerator generator = new(new IterationOptions { IterationMode = IterationMode.Double });

        // Act
        generator.NextDouble(0.0, 1.0);
        generator.NextDouble(0.0, 1.0);

        // Assert
        var iterationState = generator.GetState<SeededRandomState>().IterationState;
        Assert.NotNull(iterationState);
        Assert.Equal(IterationMode.Double, iterationState.IterationMode);
        Assert.Equal(2, iterationState.Iteration);
    }

    [Fact]
    public void Constructor_WithCapturedDoubleState_ContinuesSequence()
    {
        // Arrange
        var generator = new SeededRandomNumberGenerator(12345, new IterationOptions { IterationMode = IterationMode.Double }); 
        for (int i = 0; i < 5; i++)
        {
            generator.NextDouble(0.0, 1.0);
        }

        var state = generator.GetState<SeededRandomState>();
        var restoredGenerator = new SeededRandomNumberGenerator(state);

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
    public void Constructor_WithCapturedIntegerState_ContinuesSequence()
    {
        // Arrange
        var generator = new SeededRandomNumberGenerator(12345, new IterationOptions { IterationMode = IterationMode.Integer });
        for (int i = 0; i < 5; i++)
        {
            generator.NextInteger(0, 100);
        }

        var state = generator.GetState<SeededRandomState>();
        var restoredGenerator = new SeededRandomNumberGenerator(state);

        // Act
        var expectedValues = new int[5];
        var restoredValues = new int[5];
        for (int i = 0; i < expectedValues.Length; i++)
        {
            expectedValues[i] = generator.NextInteger(0, 100);
            restoredValues[i] = restoredGenerator.NextInteger(0, 100);
        }

        // Assert
        Assert.Equal(expectedValues, restoredValues);
    }

    [Fact]
    public void Clone_CreatesGeneratorAtCurrentTrackedState()
    {
        // Arrange
        var generator = new SeededRandomNumberGenerator(12345, new IterationOptions { IterationMode = IterationMode.Double });
        for (int i = 0; i < 5; i++)
        {
            generator.NextDouble(0.0, 1.0);
        }

        // Act
        var clone = generator.Clone();

        // Assert
        Assert.Equal(generator.NextDouble(0.0, 1.0), clone.NextDouble(0.0, 1.0));
    }

    #endregion
}
