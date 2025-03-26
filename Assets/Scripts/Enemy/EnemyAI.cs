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
        // ����ߴٸ� �ı��Ǳ� ������ �ƹ� �ൿ�� ���� ����
        if (enemyStatus.isDie) return;

        // �÷��̾ ����ߴٸ�
        if(player.Status.isDie)
        {
            // �ִϸ��̼� Idle�� ���� �� AI ����
            AllStopAnimation();
            agent.isStopped = true;
            return;
        }

        // ���� ��Ÿ�� ���
        attackTimer -= Time.deltaTime;

        // �÷��̾���� �Ÿ�
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // �÷��̾���� �Ÿ��� 5 ���϶�� �÷��̾� ����
        if (distanceToPlayer < 5f)
        {
            // ���� �����Ÿ��� ���Դٸ� ����
            if (distanceToPlayer <= attackRange)
            {
                AttackTarget();
            }
            // ������ �ʾҴٸ� �÷��̾�� �̵�
            else
            {
                MoveToTarget();
            }
        }
        // �÷��̾ �������� �ʾҴٸ� Idle ����
        else
            TransitionToIdle();
    }

    // �÷��̾� ��ġ�� �̵��ϴ� �Լ�
    private void MoveToTarget()
    {
        // �÷��̾� ��ġ�� ��� ����
        agent.SetDestination(player.transform.position);

        // �̵� �ִϸ��̼� ���
        StartAnimation(animationData.RunParameterHash);
        StopAnimation(animationData.IdleParameterHash);
    }

    // �÷��̾ �����ϴ� �Լ�
    private void AttackTarget()
    {
        // ���� ��� �ʱ�ȭ
        agent.ResetPath();
        StopAnimation(animationData.RunParameterHash);

        // ���� ������ ��Ÿ���� �����ٸ�
        if (attackTimer <= 0f)
        {
            // ���� �� Ÿ�̸� �ʱ�ȭ
            attackTimer = attackCooldown;

            StartAnimation(animationData.AttackParameterHash);
        }
    }

    // Idle ���� �Լ�
    private void TransitionToIdle()
    {
        agent.ResetPath();
        StopAnimation(animationData.RunParameterHash);
        StartAnimation(animationData.IdleParameterHash);
    }

    // ���� �ִϸ��̼� ��� �� �÷��̾�� ������ �ֱ�(�ִϸ��̼� �̺�Ʈ�� ����)
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

    // Idle ���� ��� �ִϸ��̼� ���� �Լ�
    public void AllStopAnimation()
    {
        StartAnimation(animationData.IdleParameterHash);
        StopAnimation(animationData.RunParameterHash);
        StopAnimation(animationData.AttackParameterHash);
    }

    // AI ���� �� ��� �ִϸ��̼� ��� �Լ�
    public void StopAI()
    {
        col.isTrigger = true;

        if (agent != null)
            agent.enabled = false;

        animator.SetTrigger("Die");
    }
}