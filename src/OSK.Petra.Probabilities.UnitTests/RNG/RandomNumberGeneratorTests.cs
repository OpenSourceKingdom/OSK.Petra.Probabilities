using OSK.Petra.Probabilities.Ports;

namespace OSK.Petra.Probabilities.UnitTests.RNG;

public abstract class RandomNumberGeneratorTests<TGenerator>
    where TGenerator: IRandomNumberGenerator
{
    #region NextDouble

    [Fact]
    public void NextDouble_ReturnsValueBetweenZeroAndOne()
    {
        // Arrange
        var numberGenerator = CreateDefaultGenerator();
        
        //Act & Assert
        for (int i = 0; i < 100; i++)
        {
            var value = numberGenerator.NextDouble(0.0, 1.0);
            Assert.InRange(value, 0.0, 1.0);
        }
    }

    [Fact]
    public void NextDouble_ProducesDistinctValues()
    {
        // Arrange
        var numberGenerator = CreateDefaultGenerator();
        HashSet<double> values = [];

        // Act
        for (int i = 0; i < 100; i++)
        {
            values.Add(numberGenerator.NextDouble(0.0, 1.0));
        }

        // Assert
        Assert.True(values.Count > 90, "Should produce mostly distinct values");
    }

    [Fact]
    public void NextDouble_StatisticallySpreadsAcrossRange()
    {
        // Arrange
        var numberGenerator = CreateDefaultGenerator();
        int lowCount = 0;
        int highCount = 0;

        // Act
        for (int i = 0; i < 1000; i++)
        {
            var value = numberGenerator.NextDouble(0.0, 1.0);
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

    #region NextInteger

    [Fact]
    public void NextInteger_ReturnsValueWithinRange()
    {
        // Arrange
        var numberGenerator = CreateDefaultGenerator();
        int min = 1;
        int max = 10;

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            int value = numberGenerator.NextInteger(min, max);
            Assert.InRange(value, min, max);
        }
    }

    [Fact]
    public void NextInteger_WithEqualMinMax_ReturnsMinValue()
    {
        // Arrange
        var numberGenerator = CreateDefaultGenerator();
        int value = 5;

        // Act
        int result = numberGenerator.NextInteger(value, value);

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public void NextInteger_WithMinGreaterThanMax_ThrowsInvalidOperationException()
    {
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => CreateDefaultGenerator().NextInteger(10, 5));
    }

    [Fact]
    public void NextInteger_ProducesDistinctValues()
    {
        // Arrange
        var numberGenerator = CreateDefaultGenerator();
        HashSet<int> values = [];

        // Act
        for (int i = 0; i < 50; i++)
        {
            values.Add(numberGenerator.NextInteger(0, 1000));
        }

        // Assert
        Assert.True(values.Count > 40, "Should produce mostly distinct values");
    }

    [Fact]
    public void NextInteger_WithNegativeRange_Works()
    {
        // Arrange
        var numberGenerator = CreateDefaultGenerator();

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            int value = numberGenerator.NextInteger(-50, -10);
            Assert.InRange(value, -50, -10);
        }
    }

    [Fact]
    public void NextInteger_WithLargeRange_Works()
    {
        // Arrange
        var numberGenerator = CreateDefaultGenerator();

        // Act & Assert
        for (int i = 0; i < 100; i++)
        {
            int value = numberGenerator.NextInteger(0, int.MaxValue - 1);
            Assert.InRange(value, 0, int.MaxValue - 1);
        }
    }

    #endregion

    #region Helpers

    protected abstract TGenerator CreateDefaultGenerator();

    #endregion
}
