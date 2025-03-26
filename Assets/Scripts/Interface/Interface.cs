using System.Numerics;

// 소모 아이템 인터페이스
public interface IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0);
}

// 데미지 입는 오브젝트 인터페이스
public interface IDamageable
{
    void OnDamaged(BigInteger damage);
    public bool isDie {  get; set; }
}
