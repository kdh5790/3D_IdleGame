using System.Numerics;

// �Ҹ� ������ �������̽�
public interface IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0);
}

// ������ �Դ� ������Ʈ �������̽�
public interface IDamageable
{
    void OnDamaged(BigInteger damage);
    public bool isDie {  get; set; }
}
