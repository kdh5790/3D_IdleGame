using System.Numerics;
using UnityEngine;

[System.Serializable]
public class HealthRecoveryPotion : IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0)
    {
        Player.Instance.Status.CurrentHealth = Player.Instance.Status.TotalMaxHealth;
    }
}

[System.Serializable]
public class GoldBoostPotion : IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0)
    {
        if(percentage == 0 || duration == 0)
        {
            Debug.LogError("���� ȿ���� �������� �ʾҽ��ϴ�.");
            return;
        }

        Player.Instance.Status.IncreasedBonusGold(percentage, duration);
    }
}

[System.Serializable]
public class DamageBoostPotion : IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0)
    {
        if (percentage == 0 || duration == 0)
        {
            Debug.LogError("���� ȿ���� �������� �ʾҽ��ϴ�.");
            return;
        }

        Player.Instance.Status.IncreasedBonusDamage(percentage, duration);
    }
}