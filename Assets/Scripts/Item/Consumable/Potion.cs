using System.Numerics;
using UnityEngine;

// 포션들을 클래스별로 생성 후 효과 적용 함수 추가
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
            Debug.LogError("포션 효과가 설정되지 않았습니다.");
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
            Debug.LogError("포션 효과가 설정되지 않았습니다.");
            return;
        }

        Player.Instance.Status.IncreasedBonusDamage(percentage, duration);
    }
}