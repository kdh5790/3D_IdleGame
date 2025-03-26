using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item")]
public class ConsumableItemData : BaseItemData
{
    [field:SerializeReference]
    public IConsumableItem item;

    public float effectValue;   // ȿ�� �� (��: ȸ����, ������ ��)
    public float duration;      // ���� �ð� (0�̸� ��� ȿ��)

    public void Use()
    {
        if (item != null)
        {
            item.UsePotion(effectValue, duration); // �������̽� �޼��� ȣ��
        }
        else
        {
            Debug.LogError("���� ȿ���� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
}