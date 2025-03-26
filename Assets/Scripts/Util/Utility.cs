using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public static class Utility
{
    public static string FormatBigNumber(BigInteger number)
    {
        // 숫자가 1000보다 작으면 그대로 문자열로 반환
        if (number < 1000)
        {
            return number.ToString();
        }

        // 단위를 나타낼 문자열 배열
        string[] units = { "", "k", "m", "b", "t", "q", "Q", "s", "S", "o", "n" };

        int unitIndex = 0; // 현재 단위 인덱스(units)

        decimal divisor = 1000; // number에 나눠줄 값

        // 소수점 유지를 위해 decimal로 형변환
        decimal decimalNumber = (decimal)number;

        // 숫자가 divisor 보다 크고 unitIndex가 units 배열 크기를 벗어나지 않는 동안 반복
        while (decimalNumber >= divisor && unitIndex < units.Length - 1)
        {
            decimalNumber /= divisor; // 매개변수로 받은 숫자를 divisor로 나누기
            unitIndex++; // 단위 인덱스 증가
        }

        string formatString = unitIndex == 0 ? "{0}" : "{0:0.0}{1}";

        // 숫자를 formatString에 따라 포맷하고, 단위 문자열을 추가하여 반환
        return string.Format(formatString, decimalNumber, units[unitIndex]);
    }
}
