using OSK.Petra.Probabilities.RNG.Seeded;

namespace OSK.Petra.Probabilities.UnitTests.RNG.Pseudo;

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

    #endregion
}
