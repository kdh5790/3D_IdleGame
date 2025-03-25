using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public static class Utility
{
    public static string FormatBigNumber(BigInteger number)
    {
        if (number < 1000)
        {
            return number.ToString();
        }

        string[] units = { "", "k", "m", "b", "t", "q", "Q", "s", "S", "o", "n" };
        int unitIndex = 0;
        BigInteger divisor = 1000;

        while (number >= divisor && unitIndex < units.Length - 1)
        {
            number /= divisor;
            unitIndex++;
        }

        string formatString = unitIndex == 0 ? "{0}" : "{0:0.#}{1}";
        return string.Format(formatString, number, units[unitIndex]);
    }
}
