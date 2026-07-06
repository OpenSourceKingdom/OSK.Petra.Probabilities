using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace OSK.Petra.Probabilities.RNG;

/// <summary>
/// A group of helper methods for usage with Random number generators
/// </summary>
public static class RandomHelpers
{
    /// <summary>
    /// Create a random seed using an integer
    /// </summary>
    /// <returns></returns>
    public static int CreateRandomSeed32()
    {
        var bytes = new byte[4];

        using var random = RandomNumberGenerator.Create();
        random.GetBytes(bytes);

        return BitConverter.ToInt32(bytes, 0);
    }

    /// <summary>
    /// Creates a random seed using a long
    /// </summary>
    /// <returns>The random seed</returns>
    public static ulong CreateLongRandomSeed64()
    {
        var bytes = new byte[8];

        using var random = RandomNumberGenerator.Create();
        random.GetBytes(bytes);

        return BitConverter.ToUInt64(bytes, 0);
    }
}
