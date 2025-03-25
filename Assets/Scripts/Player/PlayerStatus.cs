using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private BigInteger maxHealth = 525430; // 최대 체력 
    private BigInteger currentHealth; // 현재 체력

    [SerializeField] private BigInteger gold = 32500;
    [SerializeField] private BigInteger attackPower = 3532510; // 공격력 
    [SerializeField] private float speed; // 이동 속도

    public Action OnStatusChanged;

    public BigInteger MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = BigInteger.Max(0, value);
            OnStatusChanged?.Invoke();
        }
    }

    public BigInteger CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = BigInteger.Max(0, value);
            OnStatusChanged?.Invoke();
        }
    }

    public BigInteger Gold
    {
        get => gold;
        set
        {
            gold = BigInteger.Max(0, value);
            OnStatusChanged?.Invoke();
        }
    }

    public BigInteger AttackPower
    {
        get => attackPower;
        set
        {
            attackPower = BigInteger.Max(0, value);
            OnStatusChanged?.Invoke();
        }
    }

    public float Speed
    {
        get => speed;
        set => speed = Mathf.Max(0, value);
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }
}