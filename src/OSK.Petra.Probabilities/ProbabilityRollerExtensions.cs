using OSK.Petra.Probabilities.Ports;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Petra.Probabilities;

public static class ProbabilityRollerExtensions
{
    extension(IProbabilityRoller roller)
    {
        #region Dice

        /// <summary>
        /// Rolls a die and returns the result.
        /// </summary>
        /// <param name="sides">The total number of sides on the die.</param>
        /// <returns>The value the die landed on.</returns>
        public int RollDie(int sides)
            => roller.Roll(1, sides);

        /// <summary>
        /// Rolls a collection of dice that all use the same number of sides.
        /// </summary>
        /// <param name="sides">The total number of sides on each die.</param>
        /// <param name="totalDice">The total number of dice being rolled.</param>
        /// <returns>An array containing the values for each die.</returns>
        public int[] RollDice(int sides, int totalDice)
            => [.. roller.RollDice(Enumerable.Repeat(sides, totalDice))];

        /// <summary>
        /// Rolls a collection of dice, each using varying numbers of sides.
        /// </summary>
        /// <param name="dice">The collection of dice to roll.</param>
        /// <returns>An array containing the values for each die.</returns>
        public int[] RollDice(IEnumerable<int> dice)
            => [.. dice.Select(roller.RollDie)];

        #endregion

        #region Chance

        /// <summary>
        /// Rolls for success using a probability threshold between 0 and 100.
        /// </summary>
        /// <param name="probabilityRequired">The probability required to succeed.</param>
        /// <returns>Whether the chance succeeded.</returns>
        public bool Chance(int probabilityRequired)
            => roller.Chance(0, 100, probabilityRequired);

        /// <summary>
        /// Validates whether a chance check succeeds within the specified range.
        /// </summary>
        /// <param name="minChance">The minimum chance value.</param>
        /// <param name="maxChance">The maximum chance value.</param>
        /// <param name="probabilityRequired">The probability threshold required to succeed.</param>
        /// <returns>Whether the chance succeeded.</returns>
        public bool Chance(int minChance, int maxChance, int probabilityRequired)
            => probabilityRequired >= roller.Roll(minChance, maxChance);

        /// <summary>
        /// Rolls for success using a probability threshold between 0.0 and 1.0.
        /// </summary>
        /// <param name="probabilityRequired">The probability required to succeed.</param>
        /// <returns>Whether the chance succeeded.</returns>
        public bool Chance(double probabilityRequired)
            => roller.Chance(0, 1, probabilityRequired);

        /// <summary>
        /// Validates whether a chance check succeeds within the specified range.
        /// </summary>
        /// <param name="minChance">The minimum chance value.</param>
        /// <param name="maxChance">The maximum chance value.</param>
        /// <param name="probabilityRequired">The probability threshold required to succeed.</param>
        /// <returns>Whether the chance succeeded.</returns>
        public bool Chance(double minChance, double maxChance, double probabilityRequired)
            => probabilityRequired >= roller.Roll(minChance, maxChance);

        #endregion
    }
}