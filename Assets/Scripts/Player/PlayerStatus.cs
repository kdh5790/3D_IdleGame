using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int maxHealth; // 최대 체력 
    private int currentHealth; // 현재 체력

    [SerializeField] private float attackPower; // 공격력 
    [SerializeField] private float speed; // 이동 속도

    public int MaxHealth => maxHealth;
    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }

    public float AttackPower
    {
        get => attackPower;
        set => attackPower = Mathf.Max(0, value);
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