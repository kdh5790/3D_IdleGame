using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item")]
public class ConsumableItemData : BaseItemData
{
    public enum ConsumableType
    {
        HealthRecovery, // 체력 회복
        GoldBoost,      // 골드 획득량 증가
        DamageBoost     // 피해량 증가
    }

    [Header("Consumable Properties")]
    public ConsumableType type; // 소비 아이템 타입
    public float effectValue;   // 효과 값 (예: 회복량, 증가량 등)
    public float duration;      // 지속 시간 (0이면 즉시 효과)
}
