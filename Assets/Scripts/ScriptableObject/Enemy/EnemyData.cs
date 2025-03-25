using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;

    public BigInteger maxHealth;
    public BigInteger currentHealth;

    public BigInteger damage;

    public GameObject enemyPrefab;
}
