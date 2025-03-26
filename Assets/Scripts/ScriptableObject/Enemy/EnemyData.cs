using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;

    // BigInteger로 변환하기 위한 문자열(반드시 숫자로 입력)
    public string maxHealthString; 
    public string currentHealthString;
    public string damageString;
    public string dropGoldString;

    public List<BaseItemData> dropItemList; // 드랍 아이템 목록

    public BigInteger maxHealth => BigInteger.Parse(maxHealthString);
    public BigInteger currentHealth => BigInteger.Parse(currentHealthString);
    public BigInteger damage => BigInteger.Parse(damageString);
    public BigInteger dropGold => BigInteger.Parse(dropGoldString);
}
