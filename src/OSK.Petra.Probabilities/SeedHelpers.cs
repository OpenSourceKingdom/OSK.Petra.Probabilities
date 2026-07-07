using System;
using System.Security.Cryptography;

namespace OSK.Petra.Probabilities;

/// <summary>
/// A group of helper methods to generate seeds with Random number generators
/// </summary>
public static class SeedHelpers
{
    /// <summary>
    /// Create a random seed using an integer
    /// </summary>
    /// <returns></returns>
    public static int RandomSeed32()
    {
        var bytes = new byte[4];

        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(bytes);

        return BitConverter.ToInt32(bytes, 0);
    }

    /// <summary>
    /// Creates a random seed using a long
    /// </summary>
    /// <returns>The random seed</returns>
    public static ulong RandomSeed64()
    {
        var bytes = new byte[8];

        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(bytes);

        return BitConverter.ToUInt64(bytes, 0);
    }
}
