using OSK.Petra.Probabilities.Ports;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Petra.Probabilities;

public static class ProbabilityRollerExtensions
{
    #region Dice

    /// <summary>
    /// Rolls a dice and returns the result
    /// </summary>
    /// <param name="roller">The roller to use</param>
    /// <param name="sides">The total sides for the die</param>
    /// <returns>The value the die landed on</returns>
    public static int RollDie(this IProbabilityRoller roller, int sides)
        => roller.Roll(1, sides);

    /// <summary>
    /// Rolls a collection of dice that all use the same sides
    /// </summary>
    /// <param name="roller">The roller to use</param>
    /// <param name="sides">The total sides for the dice</param>
    /// <param name="totalDice">The total number of dice being rolled</param>
    /// <returns>An array containing the values for each dice</returns>
    public static int[] RollDice(this IProbabilityRoller roller, int sides, int totalDice)
        => [.. roller.RollDice(Enumerable.Repeat(sides, totalDice))];

    /// <summary>
    /// Rolls a collection of dice, using varying sides
    /// </summary>
    /// <param name="roller">The roller to use</param>
    /// <param name="dice">The collection of dice to roll</param>
    /// <returns>An array containing the values for each dice</returns>
    public static int[] RollDice(this IProbabilityRoller roller, IEnumerable<int> dice)
        => [.. dice.Select(roller.RollDie)];

    #endregion

    #region Chance

    /// <summary>
    /// Runs a roll check with a chance between 0.0 and 1.0 and the required probability to pass
    /// </summary>
    /// <param name="roller">The roller to use</param>
    /// <param name="probabilityRequired">The probabilistic chance required to succeed</param>
    /// <returns>Whether the chance succeeded</returns>
    public static bool Chance(this IProbabilityRoller roller, double probabilityRequired)
        => roller.RollCheck(0, 1, probabilityRequired);

    #endregion
}
