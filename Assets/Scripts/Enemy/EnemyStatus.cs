using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyStatus : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData enemyData;

    private EnemyAI enemyAi;

    private BigInteger maxHealth;
    private BigInteger currentHealth;
    public BigInteger damage;

    private bool isInvincibility = false;

    public bool isDie { get; set; } = false;

    private void Start()
    {
        maxHealth = enemyData.maxHealth;
        currentHealth = enemyData.currentHealth;
        damage = enemyData.damage;

        enemyAi = GetComponent<EnemyAI>();
    }

    public void OnDamaged(BigInteger damage)
    {
        if (isInvincibility) return;

        currentHealth = BigInteger.Max(0, currentHealth - damage);
        StartCoroutine(ApplyInvincibility(0.3f));

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDie = true;

        enemyAi.StopAI();

        EnemySpawner.Instance.RemoveEnemy(gameObject);

        Destroy(gameObject, 2f); 
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

        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.material.color = color;
        }

        foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderers)
        {
            renderer.material.color = color;
        }
    }
}
