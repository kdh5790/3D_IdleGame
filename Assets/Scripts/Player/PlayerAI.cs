using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void FindTarget()
    {
        var enemies = EnemySpawner.Instance.enemies;

        if (enemies.Count == 0)
        {
            Debug.LogError("적이 존재하지 않습니다.");
            return;
        }

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // 플레이어의 현재 위치
        Vector3 playerPosition = transform.position;

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue; // 적이 null인 경우 건너뜀

            // 플레이어와 적 사이의 거리 계산
            float distance = Vector3.Distance(playerPosition, enemy.transform.position);

            // 가장 가까운 적 업데이트
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            // 가장 가까운 적으로 이동
            agent.SetDestination(closestEnemy.transform.position);
        }
    }
}