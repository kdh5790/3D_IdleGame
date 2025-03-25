using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Player player;
    private PlayerAnimationData animationData;

    [SerializeField] private float attackRange = 2f; // 공격 거리
    [SerializeField] private float attackCooldown = 2f; // 공격 쿨타임
    private float attackTimer = 0f; // 공격 타이머

    private GameObject currentTarget; // 현재 타겟

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Player.Instance;
        animationData = player.AnimationData;

        animationData.Initialize();
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime; // 공격 타이머 감소

        FindClosestTarget(); // 가장 가까운 적을 찾음

        if (currentTarget != null) // 타겟이 있는 경우
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);

            if (distanceToTarget <= attackRange) // 적이 공격 거리 내에 있는 경우
            {
                AttackTarget();
            }
            else // 적이 공격 거리 밖에 있는 경우
            {
                MoveToTarget();
            }
        }
        else
        {
            TransitionToIdle(); // 타겟이 없으면 Idle 애니메이션으로 전환
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

        currentTarget = closestEnemy; // 가장 가까운 적을 타겟으로 설정
    }

    private void MoveToTarget()
    {
        if (currentTarget == null) return;

        agent.SetDestination(currentTarget.transform.position); // 타겟 위치로 이동
        player.StartAnimation(animationData.RunParameterHash); 
        player.StopAnimation(animationData.IdleParameterHash);
    }

    private void AttackTarget()
    {
        agent.ResetPath(); // 이동 중지
        player.StopAnimation(animationData.RunParameterHash);

        if (attackTimer <= 0f) // 공격 쿨타임 확인
        {
            attackTimer = attackCooldown; // 쿨타임 초기화

            int comboIndex = Random.Range(0, 3); // 랜덤으로 ComboAttack 파라미터 설정 

            player.StartAnimation(animationData.ComboAttackParameterHash);
            player.Anim.SetInteger(animationData.ComboParameterHash, comboIndex);
        }
    }

    private void TransitionToIdle()
    {
        agent.ResetPath(); // 이동 중지
        player.StopAnimation(animationData.RunParameterHash); 
        player.StartAnimation(animationData.IdleParameterHash);
    }

    public void ResetAttack()
    {
        player.Anim.SetBool(animationData.ComboAttackParameterHash, false); 
    }
}