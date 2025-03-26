using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Player player;
    private Animator animator;
    private Collider col;
    private EnemyStatus enemyStatus;

    [SerializeField] public EnemyAnimationData animationData;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;
    private float attackTimer = 0f;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyStatus = GetComponent<EnemyStatus>();
        col = GetComponent<Collider>();

        player = Player.Instance;

        animationData.Initialize();
    }

    private void Update()
    {
        // 사망했다면 파괴되기 전까지 아무 행동도 하지 않음
        if (enemyStatus.isDie) return;

        // 플레이어가 사망했다면
        if(player.Status.isDie)
        {
            // 애니메이션 Idle로 변경 후 AI 정지
            AllStopAnimation();
            agent.isStopped = true;
            return;
        }

        // 공격 쿨타임 계산
        attackTimer -= Time.deltaTime;

        // 플레이어와의 거리
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // 플레이어와의 거리가 5 이하라면 플레이어 감지
        if (distanceToPlayer < 5f)
        {
            // 공격 사정거리에 들어왔다면 공격
            if (distanceToPlayer <= attackRange)
            {
                AttackTarget();
            }
            // 들어오지 않았다면 플레이어에게 이동
            else
            {
                MoveToTarget();
            }
        }
        // 플레이어가 감지되지 않았다면 Idle 유지
        else
            TransitionToIdle();
    }

    // 플레이어 위치로 이동하는 함수
    private void MoveToTarget()
    {
        // 플레이어 위치로 경로 설정
        agent.SetDestination(player.transform.position);

        // 이동 애니메이션 재생
        StartAnimation(animationData.RunParameterHash);
        StopAnimation(animationData.IdleParameterHash);
    }

    // 플레이어를 공격하는 함수
    private void AttackTarget()
    {
        // 현재 경로 초기화
        agent.ResetPath();
        StopAnimation(animationData.RunParameterHash);

        // 공격 가능한 쿨타임이 지났다면
        if (attackTimer <= 0f)
        {
            // 공격 후 타이머 초기화
            attackTimer = attackCooldown;

            StartAnimation(animationData.AttackParameterHash);
        }
    }

    // Idle 유지 함수
    private void TransitionToIdle()
    {
        agent.ResetPath();
        StopAnimation(animationData.RunParameterHash);
        StartAnimation(animationData.IdleParameterHash);
    }

    // 공격 애니메이션 취소 및 플레이어에게 데미지 주기(애니메이션 이벤트로 실행)
    public void ResetAttack()
    {
        StopAnimation(animationData.AttackParameterHash);
        Player.Instance.Status.OnDamaged(enemyStatus.damage);
    }

    public void StartAnimation(int animatorHash)
    {
        animator.SetBool(animatorHash, true);
    }

    public void StopAnimation(int animatorHash)
    {
        animator.SetBool(animatorHash, false);
    }

    // Idle 제외 모든 애니메이션 정지 함수
    public void AllStopAnimation()
    {
        StartAnimation(animationData.IdleParameterHash);
        StopAnimation(animationData.RunParameterHash);
        StopAnimation(animationData.AttackParameterHash);
    }

    // AI 정지 및 사망 애니메이션 재생 함수
    public void StopAI()
    {
        col.isTrigger = true;

        if (agent != null)
            agent.enabled = false;

        animator.SetTrigger("Die");
    }
}