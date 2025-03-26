using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public static class Utility
{
    public static string FormatBigNumber(BigInteger number)
    {
        // ���ڰ� 1000���� ������ �״�� ���ڿ��� ��ȯ
        if (number < 1000)
        {
            return number.ToString();
        }

        // ������ ��Ÿ�� ���ڿ� �迭
        string[] units = { "", "k", "m", "b", "t", "q", "Q", "s", "S", "o", "n" };

        int unitIndex = 0; // ���� ���� �ε���(units)

        decimal divisor = 1000; // number�� ������ ��

        // �Ҽ��� ������ ���� decimal�� ����ȯ
        decimal decimalNumber = (decimal)number;

        // ���ڰ� divisor ���� ũ�� unitIndex�� units �迭 ũ�⸦ ����� �ʴ� ���� �ݺ�
        while (decimalNumber >= divisor && unitIndex < units.Length - 1)
        {
            decimalNumber /= divisor; // �Ű������� ���� ���ڸ� divisor�� ������
            unitIndex++; // ���� �ε��� ����
        }

        string formatString = unitIndex == 0 ? "{0}" : "{0:0.0}{1}";

        // ���ڸ� formatString�� ���� �����ϰ�, ���� ���ڿ��� �߰��Ͽ� ��ȯ
        return string.Format(formatString, decimalNumber, units[unitIndex]);
    }
}
