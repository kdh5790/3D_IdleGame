using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Items/Equipment Item")]
public class EquipmentItemData : BaseItemData
{
    public enum EquipmentType
    {
        Weapon,   // 무기
        Armor     // 방어구
    }

    [Header("Equipment Properties")]
    public EquipmentType type; // 장비 타입

    [Header("Stat Bonuses")]
    public float healthBonus;      // 체력 보너스
    public float goldBonus;        // 골드 획득량 보너스
    public float attackPowerBonus; // 공격력 보너스
}
