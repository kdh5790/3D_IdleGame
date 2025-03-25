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
            item.UsePotion(); // 인터페이스 메서드 호출
        }
        else
        {
            Debug.LogError("포션 효과가 할당되지 않았습니다.");
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
        EditorUtility.SetDirty(data); // 데이터 변경 사항 저장
        AssetDatabase.SaveAssets();  // 프로젝트 파일에 변경 사항 적용
    }
}
