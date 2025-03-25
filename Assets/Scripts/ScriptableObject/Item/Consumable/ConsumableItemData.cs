using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item")]
public class ConsumableItemData : BaseItemData
{
    public enum ConsumableType
    {
        HealthRecovery, // ü�� ȸ��
        GoldBoost,      // ��� ȹ�淮 ����
        DamageBoost     // ���ط� ����
    }

    [Header("Consumable Properties")]
    public ConsumableType type; // �Һ� ������ Ÿ��
    public float effectValue;   // ȿ�� �� (��: ȸ����, ������ ��)
    public float duration;      // ���� �ð� (0�̸� ��� ȿ��)
}
