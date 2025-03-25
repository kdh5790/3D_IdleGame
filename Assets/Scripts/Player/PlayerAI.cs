using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Player player;
    private PlayerAnimationData animationData;

    [SerializeField] private float attackRange = 2f; // ���� �Ÿ�
    [SerializeField] private float attackCooldown = 2f; // ���� ��Ÿ��
    private float attackTimer = 0f; // ���� Ÿ�̸�

    private GameObject currentTarget; // ���� Ÿ��

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Player.Instance;
        animationData = player.AnimationData;

        animationData.Initialize();
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime; // ���� Ÿ�̸� ����

        FindClosestTarget(); // ���� ����� ���� ã��

        if (currentTarget != null) // Ÿ���� �ִ� ���
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);

            if (distanceToTarget <= attackRange) // ���� ���� �Ÿ� ���� �ִ� ���
            {
                AttackTarget();
            }
            else // ���� ���� �Ÿ� �ۿ� �ִ� ���
            {
                MoveToTarget();
            }
        }
        else
        {
            TransitionToIdle(); // Ÿ���� ������ Idle �ִϸ��̼����� ��ȯ
        }
    }

    private void FindClosestTarget()
    {
        var enemies = EnemySpawner.Instance.enemies;

        if (enemies.Count == 0)
        {
            currentTarget = null;
            return;
        }

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        currentTarget = closestEnemy; // ���� ����� ���� Ÿ������ ����
    }

    private void MoveToTarget()
    {
        if (currentTarget == null) return;

        agent.SetDestination(currentTarget.transform.position); // Ÿ�� ��ġ�� �̵�
        player.StartAnimation(animationData.RunParameterHash); 
        player.StopAnimation(animationData.IdleParameterHash);
    }

    private void AttackTarget()
    {
        agent.ResetPath(); // �̵� ����
        player.StopAnimation(animationData.RunParameterHash);

        if (attackTimer <= 0f) // ���� ��Ÿ�� Ȯ��
        {
            attackTimer = attackCooldown; // ��Ÿ�� �ʱ�ȭ

            int comboIndex = Random.Range(0, 3); // �������� ComboAttack �Ķ���� ���� 

            player.StartAnimation(animationData.ComboAttackParameterHash);
            player.Anim.SetInteger(animationData.ComboParameterHash, comboIndex);
        }
    }

    private void TransitionToIdle()
    {
        agent.ResetPath(); // �̵� ����
        player.StopAnimation(animationData.RunParameterHash); 
        player.StartAnimation(animationData.IdleParameterHash);
    }

    public void ResetAttack()
    {
        player.Anim.SetBool(animationData.ComboAttackParameterHash, false); 
    }
}