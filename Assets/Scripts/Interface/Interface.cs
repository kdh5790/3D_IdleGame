using System.Numerics;

public interface IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0);
}

public interface IDamageable
{
    void OnDamaged(BigInteger damage);
    public bool isDie {  get; set; }
}
