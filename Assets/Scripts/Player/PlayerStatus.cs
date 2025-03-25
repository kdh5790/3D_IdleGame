using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private BigInteger maxHealth = 525430; // �ִ� ü�� 
    private BigInteger currentHealth; // ���� ü��

    [SerializeField] private BigInteger gold = 32500;
    [SerializeField] private BigInteger attackPower = 3532510; // ���ݷ� 
    [SerializeField] private float speed; // �̵� �ӵ�

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