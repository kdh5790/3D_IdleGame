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
