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
            Debug.LogError("���� �������� �ʽ��ϴ�.");
            return;
        }

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // �÷��̾��� ���� ��ġ
        Vector3 playerPosition = transform.position;

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue; // ���� null�� ��� �ǳʶ�

            // �÷��̾�� �� ������ �Ÿ� ���
            float distance = Vector3.Distance(playerPosition, enemy.transform.position);

            // ���� ����� �� ������Ʈ
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            // ���� ����� ������ �̵�
            agent.SetDestination(closestEnemy.transform.position);
        }
    }
}