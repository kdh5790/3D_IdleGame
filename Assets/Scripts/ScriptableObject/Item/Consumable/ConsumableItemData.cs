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
            item.UsePotion(); // �������̽� �޼��� ȣ��
        }
        else
        {
            Debug.LogError("���� ȿ���� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
}

[CustomEditor(typeof(ConsumableItemData))]
public class ConsumableItemDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ConsumableItemData data = (ConsumableItemData)target;

        if (data.item == null)
        {
            if (GUILayout.Button("Add Behavior : Recovery"))
            {
                data.item = new HealthRecoveryPotion();
                SaveChanges(data);
            }

            if (GUILayout.Button("Add Behavior : Gold Boost"))
            {
                data.item = new GoldBoostPotion();
                SaveChanges(data);
            }

            if (GUILayout.Button("Add Behavior : Damage Boost"))
            {
                data.item = new DamageBoostPotion();
                SaveChanges(data);
            }
        }
        else
        {
            EditorGUILayout.LabelField("Current Behavior", data.item.GetType().Name);

            if (GUILayout.Button("Remove Behavior"))
            {
                data.item = null;
                SaveChanges(data);
            }
        }
    }

    private void SaveChanges(ConsumableItemData data)
    {
        EditorUtility.SetDirty(data); // ������ ���� ���� ����
        AssetDatabase.SaveAssets();  // ������Ʈ ���Ͽ� ���� ���� ����
    }
}
