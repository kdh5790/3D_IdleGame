using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int maxHealth; // �ִ� ü�� 
    private int currentHealth; // ���� ü��

    [SerializeField] private float attackPower; // ���ݷ� 
    [SerializeField] private float speed; // �̵� �ӵ�

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