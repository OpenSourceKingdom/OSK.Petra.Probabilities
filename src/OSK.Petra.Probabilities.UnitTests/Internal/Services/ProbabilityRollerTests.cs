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

    #region RollCheck

    [Fact]
    public void RollCheck_ProbabilityTooLow_ReturnsFalse()
    {
        // Arrange
        _mockRandomGenerator.Setup(m => m.NextDouble(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(50);

        // Act
        var result = _roller.RollCheck(0, 100, 75);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void RollCheck_ProbabilityExactly_ReturnsTrue()
    {
        // Arrange
        _mockRandomGenerator.Setup(m => m.NextDouble(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(50);

        // Act
        var result = _roller.RollCheck(0, 100, 50);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void RollCheck_ProbabilityExceeds_ReturnsTrue()
    {
        // Arrange
        _mockRandomGenerator.Setup(m => m.NextDouble(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(50);

        // Act
        var result = _roller.RollCheck(0, 100, 25);

        // Assert
        Assert.True(result);
    }

    #endregion
}
