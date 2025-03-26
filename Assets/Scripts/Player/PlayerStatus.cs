using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    [SerializeField] private BigInteger baseMaxHealth = 1500;
    private BigInteger currentHealth;

    [SerializeField] private BigInteger gold = 0;
    [SerializeField] private BigInteger baseAttackPower = 200;

    [SerializeField] private float equipHealthBonusPercentage = 0f; 
    [SerializeField] private float equipAttackBonusPercentage = 0f;
    [SerializeField] private float equipGoldBonusPercentage = 0f;

    private bool isInvincibility = false;
    public Action OnStatusChanged;

    public BigInteger BaseMaxHealth
    {
        get => baseMaxHealth;
        set
        {
            baseMaxHealth = BigInteger.Max(0, value);
            OnStatusChanged?.Invoke();
        }
    }

    public BigInteger TotalMaxHealth
    {
        get => (BigInteger)((float)BaseMaxHealth * (1 + equipHealthBonusPercentage / 100f));
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

    public BigInteger BaseAttackPower
    {
        get => baseAttackPower;
        set
        {
            baseAttackPower = BigInteger.Max(0, value);
            OnStatusChanged?.Invoke();
        }
    }

    public BigInteger TotalAttackPower
    {
        get => (BigInteger)((float)BaseAttackPower * (1 + equipAttackBonusPercentage / 100f));
    }

    public float EquipHealthBonusPercentage
    {
        get => equipHealthBonusPercentage;
        set
        {
            equipHealthBonusPercentage = Mathf.Max(0, value); 
            OnStatusChanged?.Invoke();
        }
    }

    public float EquipAttackBonusPercentage
    {
        get => equipAttackBonusPercentage;
        set
        {
            equipAttackBonusPercentage = Mathf.Max(0, value); 
            OnStatusChanged?.Invoke();
        }
    }

    public float EquipGoldBonusPercentage
    {
        get => equipGoldBonusPercentage;
        set
        {
            equipGoldBonusPercentage = Mathf.Max(0, value);
        }
    }

    public bool isDie { get; set; } = false;

    private void Start()
    {
        CurrentHealth = TotalMaxHealth;
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
            Die();
        }
    }

    private void Die()
    {
        Player.Instance.AI.StopAI();
        isDie = true;
        Player.Instance.Anim.SetTrigger("Die");
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

    public void IncreasedBonusGold(float percentage, float duration) => StartCoroutine(IncreasedBonusGoldCoroutine(percentage, duration));
    public void IncreasedBonusDamage(float percentage, float duration) => StartCoroutine(IncreasedBonusDamageCoroutine(percentage, duration));

    private IEnumerator IncreasedBonusGoldCoroutine(float percentage, float duration)
    {
        equipGoldBonusPercentage += percentage;
        OnStatusChanged?.Invoke();

        yield return new WaitForSeconds(duration);

        equipGoldBonusPercentage -= percentage;
        OnStatusChanged?.Invoke();
    }

    private IEnumerator IncreasedBonusDamageCoroutine(float percentage, float duration)
    {
        equipAttackBonusPercentage += percentage;
        OnStatusChanged?.Invoke();

        yield return new WaitForSeconds(duration);

        equipAttackBonusPercentage -= percentage;
        OnStatusChanged?.Invoke();
    }
}