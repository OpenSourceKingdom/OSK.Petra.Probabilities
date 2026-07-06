using OSK.Petra.Probabilities.RNG.Pseudo;

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
        Assert.NotEqual(0UL, generator.State);
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
        Assert.Equal(state, generator.State);
    }

    #endregion
}
