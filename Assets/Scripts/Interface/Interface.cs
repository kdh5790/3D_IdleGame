using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public interface IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0);
}

public interface IDamageable
{
    void OnDamaged(BigInteger damage);
}
