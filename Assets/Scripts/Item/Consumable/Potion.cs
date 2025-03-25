using System.Numerics;
using UnityEngine;

[System.Serializable]
public class HealthRecoveryPotion : IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0)
    {
        Debug.Log("회복 포션 사용!");
        Player.Instance.Status.CurrentHealth = Player.Instance.Status.TotalMaxHealth;
    }
}

[System.Serializable]
public class GoldBoostPotion : IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0)
    {
        Debug.Log("골드 부스트 포션 사용!");
    }
}

[System.Serializable]
public class DamageBoostPotion : IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0)
    {
        Debug.Log("데미지 부스트 포션 사용!");
    }
}