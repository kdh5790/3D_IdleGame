using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item")]
public class ConsumableItemData : BaseItemData
{
    [field:SerializeReference]
    public IConsumableItem item; // Ŀ���� �����͸� ���� �Ҵ�

    public float effectValue;   // ȿ�� �� 
    public float duration;      // ���� �ð�

    public void Use()
    {
        if (item != null)
        {
            item.UsePotion(effectValue, duration); // �������̽� �Լ� ȣ��
        }
        else
        {
            Debug.LogError("���� ȿ���� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
}