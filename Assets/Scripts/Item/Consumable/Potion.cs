using System.Numerics;
using UnityEngine;

[System.Serializable]
public class HealthRecoveryPotion : IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0)
    {
        Debug.Log("ȸ�� ���� ���!");
        Player.Instance.Status.CurrentHealth = Player.Instance.Status.TotalMaxHealth;
    }
}

[System.Serializable]
public class GoldBoostPotion : IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0)
    {
        Debug.Log("��� �ν�Ʈ ���� ���!");
    }
}

[System.Serializable]
public class DamageBoostPotion : IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0)
    {
        Debug.Log("������ �ν�Ʈ ���� ���!");
    }
}