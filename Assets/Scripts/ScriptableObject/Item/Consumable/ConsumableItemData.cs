using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item")]
public class ConsumableItemData : BaseItemData
{
    [field:SerializeReference]
    public IConsumableItem item;

    public float effectValue;   // 효과 값 (예: 회복량, 증가량 등)
    public float duration;      // 지속 시간 (0이면 즉시 효과)

    public void Use()
    {
        if (item != null)
        {
            item.UsePotion(effectValue, duration); // 인터페이스 메서드 호출
        }
        else
        {
            Debug.LogError("포션 효과가 할당되지 않았습니다.");
        }
    }
}