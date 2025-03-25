using UnityEngine;

public abstract class BaseItemData : ScriptableObject
{
    public string itemName; // ������ �̸�
    [TextArea] public string description; // ������ ����
    public Sprite icon; // ������ ������
}