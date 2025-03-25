using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    [SerializeField] private BigInteger maxHealth = 1500; // 최대 체력 
    private BigInteger currentHealth; // 현재 체력

    [SerializeField] private BigInteger gold = 0;
    [SerializeField] private BigInteger attackPower = 200; // 공격력 

    private bool isInvincibility = false;

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
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            OnDamaged(50);
        }
    }

    public void OnDamaged(BigInteger damage)
    {
        if (isInvincibility) return;

        CurrentHealth = BigInteger.Max(0, CurrentHealth - damage);
        StartCoroutine(ApplyInvincibility(0.3f));

        if(CurrentHealth == 0)
        {
            // 캐릭터 사망
        }
    }

    private IEnumerator ApplyInvincibility(float time)
    {
        isInvincibility = true;
        MeshColorChange(Color.red);

        yield return new WaitForSeconds(time);

        isInvincibility = false;
        MeshColorChange(Color.white);
    }

    private void MeshColorChange(Color color)
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>(true);
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(true);

        foreach(MeshRenderer renderer in meshRenderers)
        {
            renderer.material.color = color;
        }

        foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderers)
        {
            renderer.material.color = color;
        }
    }
}