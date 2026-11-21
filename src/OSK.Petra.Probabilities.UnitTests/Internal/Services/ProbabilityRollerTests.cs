using Moq;
using OSK.Petra.Probabilities.Internal.Services;
using OSK.Petra.Probabilities.Ports;

namespace OSK.Petra.Probabilities.UnitTests.Internal.Services;

public class ProbabilityRollerTests
{
    #region Variables

    private readonly Mock<IRandomNumberGenerator> _mockRandomGenerator;
    private readonly ProbabilityRoller _roller;

    #endregion

    #region Constructors

    public ProbabilityRollerTests()
    {
        _mockRandomGenerator = new();
        _roller = new(_mockRandomGenerator.Object);
    }

    #endregion

    #region Roll (int)

    [Fact]
    public void Roll_Integer_ReturnsRandomNumberFromGenerator()
    {
        // Arrange
        _mockRandomGenerator.Setup(m => m.NextInteger(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(5);

        // Act
        var result = _roller.Roll(0, 10);

        // Assert
        Assert.Equal(5, result);
    }

    #endregion

    #region Roll (double)

    [Fact]
    public void Roll_Double_ReturnsRandomNumberFromGenerator()
    {
        // Arrange
        _mockRandomGenerator.Setup(m => m.NextDouble(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(15.2);

        // Act
        var result = _roller.Roll(0, 20.5);

        // Assert
        Assert.Equal(15.2, result);
    }

    #endregion

    #region Chance

    [Fact]
    public void Chance_IntProbabilityTooLow_ReturnsFalse()
    {
        // Arrange
        _mockRandomGenerator.Setup(m => m.NextInteger(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(50);

        // Act
        var result = _roller.Chance(25);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Chance_IntProbabilityExactly_ReturnsTrue()
    {
        // Arrange
        _mockRandomGenerator.Setup(m => m.NextInteger(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(50);

        // Act
        var result = _roller.Chance(50);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Chance_IntProbabilityExceeds_ReturnsTrue()
    {
        // Arrange
        _mockRandomGenerator.Setup(m => m.NextInteger(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(50);

        // Act
        var result = _roller.Chance(75);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Chance_DoubleProbabilityTooLow_ReturnsFalse()
    {
        // Arrange
        _mockRandomGenerator.Setup(m => m.NextDouble(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(0.5);

        // Act
        var result = _roller.Chance(0.25);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Chance_DoubleProbabilityExactly_ReturnsTrue()
    {
        // Arrange
        _mockRandomGenerator.Setup(m => m.NextDouble(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(0.5);

        // Act
        var result = _roller.Chance(0.5);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Chance_DoubleProbabilityExceeds_ReturnsTrue()
    {
        // Arrange
        _mockRandomGenerator.Setup(m => m.NextDouble(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(0.5);

        // Act
        var result = _roller.Chance(0.75);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Chance_IntRange_ProbabilityThreshold_ReturnsExpectedResult()
    {
        // Arrange
        _mockRandomGenerator.Setup(m => m.NextInteger(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(50);

        // Act
        var result = _roller.Chance(0, 100, 75);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Chance_DoubleRange_ProbabilityThreshold_ReturnsExpectedResult()
    {
        // Arrange
        _mockRandomGenerator.Setup(m => m.NextDouble(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(50);

        // Act
        var result = _roller.Chance(0.0, 100.0, 25.0);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region RollDie

    [Fact]
    public void RollDie_ReturnsRolledValue()
    {
        // Arrange
        var mockRoller = new Mock<IProbabilityRoller>();
        mockRoller.Setup(m => m.Roll(1, 6)).Returns(4);

        // Act
        var result = mockRoller.Object.RollDie(6);

        // Assert
        Assert.Equal(4, result);
        mockRoller.Verify(m => m.Roll(1, 6), Times.Once);
    }

    #endregion

    #region RollDice

    [Fact]
    public void RollDice_SameSides_ReturnsAllRolledValues()
    {
        // Arrange
        var mockRoller = new Mock<IProbabilityRoller>();

        mockRoller.SetupSequence(m => m.Roll(1, 6))
            .Returns(2)
            .Returns(5)
            .Returns(3);

        // Act
        var result = mockRoller.Object.RollDice(6, 3);

        // Assert
        Assert.Equal([2, 5, 3], result);
        mockRoller.Verify(m => m.Roll(1, 6), Times.Exactly(3));
    }

    [Fact]
    public void RollDice_VaryingSides_ReturnsAllRolledValues()
    {
        // Arrange
        var mockRoller = new Mock<IProbabilityRoller>();

        mockRoller.SetupSequence(m => m.Roll(1, It.IsAny<int>()))
            .Returns(2)
            .Returns(5)
            .Returns(3);

        // Act
        var result = mockRoller.Object.RollDice([4, 6, 8]);

        // Assert
        Assert.Equal([2, 5, 3], result);

        mockRoller.Verify(m => m.Roll(1, 4), Times.Once);
        mockRoller.Verify(m => m.Roll(1, 6), Times.Once);
        mockRoller.Verify(m => m.Roll(1, 8), Times.Once);
    }

    [Fact]
    public void RollDice_EmptyCollection_ReturnsEmptyArray()
    {
        // Arrange
        var mockRoller = new Mock<IProbabilityRoller>();

        // Act
        var result = mockRoller.Object.RollDice([]);

        // Assert
        Assert.Empty(result);
        mockRoller.Verify(m => m.Roll(
            It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public void RollDice_ZeroDice_ReturnsEmptyArray()
    {
        // Arrange
        var mockRoller = new Mock<IProbabilityRoller>();

        // Act
        var result = mockRoller.Object.RollDice(6, 0);

        // Assert
        Assert.Empty(result);
        mockRoller.Verify(m => m.Roll(
            It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }

    #endregion
}
