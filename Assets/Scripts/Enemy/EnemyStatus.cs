using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyStatus : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData enemyData; // ���� ������ ��Ƶ� SO

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

    // �ǰ� �� ȣ�� �Ǵ� �Լ�
    public void OnDamaged(BigInteger damage)
    {
        // �������¶�� ����
        if (isInvincibility) return;

        // �Ű����� damage ��ŭ ������ �ο�
        currentHealth = BigInteger.Max(0, currentHealth - damage);

        // ������ �ؽ�Ʈ ����
        UIManager.Instance.ShowDamageText(gameObject, damage);

        // 0.3�ʰ� �������� ����
        StartCoroutine(ApplyInvincibility(0.3f));

        if (currentHealth == 0)
        {
            Die();
        }
    }

    // ��� �� ȣ�� �Ǵ� �Լ�
    private void Die()
    {
        isDie = true;

        // AI ����
        enemyAi.StopAI();

        // �����ʿ� ����� ����Ʈ���� �ڽ� ������Ʈ ����
        EnemySpawner.Instance.RemoveEnemy(gameObject);

        // �÷��̾� ��� ����
        Player.Instance.Status.Gold += (BigInteger)((float)enemyData.dropGold * (1 + Player.Instance.Status.EquipGoldBonusPercentage / 100f));

        BaseItemData dropItem;

        // ��� ������ Ȯ�� ���
        if (Random.Range(0, 100) < 50)
        {
            if (enemyData.dropItemList != null && enemyData.dropItemList.Count > 0)
            {
                dropItem = enemyData.dropItemList[Random.Range(0, enemyData.dropItemList.Count)];

                if (dropItem is ConsumableItemData)
                {
                    Debug.Log($"{dropItem.name} ȹ��");
                    UIManager.Instance.inventoryUI.AddItem((ConsumableItemData)dropItem);
                }
                else if (dropItem is EquipmentItemData)
                {
                    Debug.Log($"{dropItem.name} ȹ��");
                    UIManager.Instance.equipmentUI.AddItem((EquipmentItemData)dropItem);
                }
            }
        }

        Destroy(gameObject, 2f); 
    }

    // ���� ���� �ڷ�ƾ
    private IEnumerator ApplyInvincibility(float time)
    {
        isInvincibility = true;
        MeshColorChange(Color.red);

        yield return new WaitForSeconds(time);

        isInvincibility = false;
        MeshColorChange(Color.white);
    }

    // ��Ƽ���� ���� ���� �Լ�
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
