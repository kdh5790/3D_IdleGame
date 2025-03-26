using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;

    public string maxHealthString;
    public string currentHealthString;
    public string damageString;

    public string dropGoldString;

    public List<BaseItemData> dropItemList;

    public BigInteger maxHealth => BigInteger.Parse(maxHealthString);
    public BigInteger currentHealth => BigInteger.Parse(currentHealthString);
    public BigInteger damage => BigInteger.Parse(damageString);
    public BigInteger dropGold => BigInteger.Parse(dropGoldString);
}
