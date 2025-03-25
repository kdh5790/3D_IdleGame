using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumableItem
{
    public void UsePotion(float percentage = 0, float duration = 0);
}
