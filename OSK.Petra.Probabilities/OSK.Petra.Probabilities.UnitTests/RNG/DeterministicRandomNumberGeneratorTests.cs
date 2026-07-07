using OSK.Petra.Probabilities.Ports;

namespace OSK.Petra.Probabilities.UnitTests.RNG;

public abstract class DeterministicRandomNumberGeneratorTests<TGenerator> : RandomNumberGeneratorTests<TGenerator>
    where TGenerator : IRandomNumberGenerator
{
    #region Determinism

    [Fact]
    public void Determinism_MultipleGenerators_CreateIdenticalRandomSequenceGeneration()
    {
        // Arrange
        var generatorA = CreateDefaultGenerator();
        var generatorB = CreateDefaultGenerator();

        // Act
        var integerValuesA = new int[10];
        var integerValuesB = new int[10];
        for (int i = 0; i < 10; i++)
        {
            integerValuesA[i] = generatorA.NextInteger(0, 1);
            integerValuesB[i] = generatorB.NextInteger(0, 1);
        }

        var doubleValuesA = new double[10];
        var doubleValuesB = new double[10];
        for (int i = 0; i < 10; i++)
        {
            doubleValuesA[i] = generatorA.NextDouble(0.0, 1.0);
            doubleValuesB[i] = generatorB.NextDouble(0.0, 1.0);
        }

        // Assert
        Assert.Equal(integerValuesA, integerValuesB);
        Assert.Equal(doubleValuesA, doubleValuesB);
    }

    #endregion
}
