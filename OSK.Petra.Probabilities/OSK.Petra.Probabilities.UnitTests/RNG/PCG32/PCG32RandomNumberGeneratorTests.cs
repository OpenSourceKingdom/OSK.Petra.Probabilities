using OSK.Petra.Probabilities.RNG.PCG32;
using Xunit;

namespace OSK.Petra.Probabilities.UnitTests.RNG.PCG32;

public class PCG32RandomNumberGeneratorTests
{
    #region Constructors

    [Fact]
    public void Constructor_WithoutSeed_InitializesGenerator()
    {
        // Arrange & Act
        var generator = new PCG32RandomNumberGenerator();

        // Assert
        Assert.NotNull(generator);
        Assert.NotEqual(0UL, generator.State);
    }

    [Fact]
    public void Constructor_WithSeed_InitializesGeneratorWithGivenSeed()
    {
        // Arrange
        ulong state = 12345UL;

        // Act
        var generator = new PCG32RandomNumberGenerator(state);

        // Assert
        Assert.NotNull(generator);
        Assert.Equal(state, generator.State);
    }

    [Fact]
    public void Constructor_WithSameSeed_ProducesSameSequence()
    {
        // Arrange
        ulong seed = 98765UL;
        var generator1 = new PCG32RandomNumberGenerator(seed);
        var generator2 = new PCG32RandomNumberGenerator(seed);

        // Act
        var values1 = new double[10];
        var values2 = new double[10];
        for (int i = 0; i < 10; i++)
        {
            values1[i] = generator1.NextDouble();
            values2[i] = generator2.NextDouble();
        }

        // Assert
        Assert.Equal(values1, values2);
    }

    #endregion

    #region NextDouble

    [Fact]
    public void NextDouble_ReturnsValueBetweenZeroAndOne()
    {
        // Arrange
        var generator = new PCG32RandomNumberGenerator(42UL);

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            var value = generator.NextDouble();
            Assert.InRange(value, 0.0, 1.0);
        }
    }

    [Fact]
    public void NextDouble_ProducesDistinctValues()
    {
        // Arrange
        var generator = new PCG32RandomNumberGenerator(42UL);
        var values = new System.Collections.Generic.HashSet<double>();

        // Act
        for (int i = 0; i < 100; i++)
        {
            values.Add(generator.NextDouble());
        }

        // Assert
        Assert.True(values.Count > 90, "Should produce mostly distinct values");
    }

    [Fact]
    public void NextDouble_StatisticallySpreadsAcrossRange()
    {
        // Arrange
        var generator = new PCG32RandomNumberGenerator(42UL);
        int lowCount = 0;
        int highCount = 0;

        // Act
        for (int i = 0; i < 1000; i++)
        {
            var value = generator.NextDouble();
            if (value < 0.5)
                lowCount++;
            else
                highCount++;
        }

        // Assert - roughly half should be in each range (with reasonable tolerance)
        Assert.InRange(lowCount, 400, 600);
        Assert.InRange(highCount, 400, 600);
    }

    #endregion

    #region Next

    [Fact]
    public void Next_ReturnsValueWithinRange()
    {
        // Arrange
        var generator = new PCG32RandomNumberGenerator(42UL);
        int min = 1;
        int max = 10;

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            int value = generator.Next(min, max);
            Assert.InRange(value, min, max);
        }
    }

    [Fact]
    public void Next_WithEqualMinMax_ReturnsMinValue()
    {
        // Arrange
        var generator = new PCG32RandomNumberGenerator(42UL);
        int value = 5;

        // Act
        int result = generator.Next(value, value);

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public void Next_WithMinGreaterThanMax_ThrowsInvalidOperationException()
    {
        // Arrange
        var generator = new PCG32RandomNumberGenerator(42UL);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => generator.Next(10, 5));
    }

    [Fact]
    public void Next_ProducesDistinctValues()
    {
        // Arrange
        var generator = new PCG32RandomNumberGenerator(42UL);
        var values = new System.Collections.Generic.HashSet<int>();

        // Act
        for (int i = 0; i < 50; i++)
        {
            values.Add(generator.Next(0, 1000));
        }

        // Assert
        Assert.True(values.Count > 40, "Should produce mostly distinct values");
    }

    [Fact]
    public void Next_WithNegativeRange_Works()
    {
        // Arrange
        var generator = new PCG32RandomNumberGenerator(42UL);

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            int value = generator.Next(-50, -10);
            Assert.InRange(value, -50, -10);
        }
    }

    [Fact]
    public void Next_WithLargeRange_Works()
    {
        // Arrange
        var generator = new PCG32RandomNumberGenerator(42UL);

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            int value = generator.Next(0, int.MaxValue - 1);
            Assert.InRange(value, 0, int.MaxValue - 1);
        }
    }

    #endregion
}
