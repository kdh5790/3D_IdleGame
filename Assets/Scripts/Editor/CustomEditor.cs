using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DungeonGenerator))]
public class DungeonGeneratorButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DungeonGenerator dungeonGenerator = (DungeonGenerator)target;

        if (GUILayout.Button("Generate Dungeon"))
            dungeonGenerator.GenerateDungeonButton();

        if (GUILayout.Button("Destroy Dungeon"))
            dungeonGenerator.DestroyDungeonButton();
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

