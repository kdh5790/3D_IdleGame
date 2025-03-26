using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyStatus : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData enemyData; // 몬스터 정보를 담아둔 SO

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

    // 피격 시 호출 되는 함수
    public void OnDamaged(BigInteger damage)
    {
        // 무적상태라면 종료
        if (isInvincibility) return;

        // 매개변수 damage 만큼 데미지 부여
        currentHealth = BigInteger.Max(0, currentHealth - damage);

        // 데미지 텍스트 생성
        UIManager.Instance.ShowDamageText(gameObject, damage);

        // 0.3초간 무적상태 유지
        StartCoroutine(ApplyInvincibility(0.3f));

        if (currentHealth == 0)
        {
            Die();
        }
    }

    // 사망 시 호출 되는 함수
    private void Die()
    {
        isDie = true;

        // AI 정지
        enemyAi.StopAI();

        // 스포너에 저장된 리스트에서 자신 오브젝트 삭제
        EnemySpawner.Instance.RemoveEnemy(gameObject);

        // 플레이어 골드 증가
        Player.Instance.Status.Gold += (BigInteger)((float)enemyData.dropGold * (1 + Player.Instance.Status.EquipGoldBonusPercentage / 100f));

        BaseItemData dropItem;

        // 드랍 아이템 확률 계산
        if (Random.Range(0, 100) < 50)
        {
            if (enemyData.dropItemList != null && enemyData.dropItemList.Count > 0)
            {
                dropItem = enemyData.dropItemList[Random.Range(0, enemyData.dropItemList.Count)];

                if (dropItem is ConsumableItemData)
                {
                    Debug.Log($"{dropItem.name} 획득");
                    UIManager.Instance.inventoryUI.AddItem((ConsumableItemData)dropItem);
                }
                else if (dropItem is EquipmentItemData)
                {
                    Debug.Log($"{dropItem.name} 획득");
                    UIManager.Instance.equipmentUI.AddItem((EquipmentItemData)dropItem);
                }
            }
        }

        Destroy(gameObject, 2f); 
    }

    // 무적 적용 코루틴
    private IEnumerator ApplyInvincibility(float time)
    {
        isInvincibility = true;
        MeshColorChange(Color.red);

        yield return new WaitForSeconds(time);

        isInvincibility = false;
        MeshColorChange(Color.white);
    }

    // 머티리얼 색상 변경 함수
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
