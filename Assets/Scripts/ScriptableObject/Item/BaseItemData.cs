using UnityEngine;

public abstract class BaseItemData : ScriptableObject
{
    public string itemName; // 아이템 이름
    [TextArea] public string description; // 아이템 설명
    public Sprite icon; // 아이템 아이콘
}