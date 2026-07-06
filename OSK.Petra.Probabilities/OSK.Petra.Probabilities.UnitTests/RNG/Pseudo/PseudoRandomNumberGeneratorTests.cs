using OSK.Petra.Probabilities.RNG.Pseudo;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Petra.Probabilities.UnitTests.RNG.Pseudo;

public class PseudoRandomNumberGeneratorTests: DeterministicRandomNumberGeneratorTests<PseudoRandomNumberGenerator>
{

    #region DeterministicRandomNumberGeneratorOverrides

    protected override PseudoRandomNumberGenerator CreateDefaultGenerator()
        => new(42);

    #endregion

    #region Constructor Tests

    [Fact]
    public void Constructor_WithoutSeed_InitializesGenerator()
    {
        // Arrange & Act
        PseudoRandomNumberGenerator generator = new();

        // Assert
        Assert.NotNull(generator);
        Assert.NotEqual(0, generator.Seed);
    }

    [Fact]
    public void Constructor_WithSeed_InitializesGeneratorWithGivenSeed()
    {
        // Arrange
        int seed = 12345;

        // Act
        PseudoRandomNumberGenerator generator = new(seed);

        // Assert
        Assert.NotNull(generator);
        Assert.Equal(seed, generator.Seed);
    }

    #endregion
}
