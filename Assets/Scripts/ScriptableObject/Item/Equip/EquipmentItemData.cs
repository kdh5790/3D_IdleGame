using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Items/Equipment Item")]
public class EquipmentItemData : BaseItemData
{
    public enum EquipmentType
    {
        Weapon,   // ����
        Armor     // ��
    }

    [Header("Equipment Properties")]
    public EquipmentType type; // ��� Ÿ��

    [Header("Stat Bonuses")]
    public float healthBonus;      // ü�� ���ʽ�
    public float goldBonus;        // ��� ȹ�淮 ���ʽ�
    public float attackPowerBonus; // ���ݷ� ���ʽ�
}
