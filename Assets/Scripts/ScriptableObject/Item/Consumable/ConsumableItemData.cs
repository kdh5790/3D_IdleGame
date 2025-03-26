using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item")]
public class ConsumableItemData : BaseItemData
{
    [field:SerializeReference]
    public IConsumableItem item; // 커스텀 에디터를 통해 할당

    public float effectValue;   // 효과 값 
    public float duration;      // 지속 시간

    public void Use()
    {
        if (item != null)
        {
            item.UsePotion(effectValue, duration); // 인터페이스 함수 호출
        }
        else
        {
            Debug.LogError("포션 효과가 할당되지 않았습니다.");
        }
    }
}