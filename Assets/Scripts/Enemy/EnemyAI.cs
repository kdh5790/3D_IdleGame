using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Player player;
    private EnemyAnimationData animationData;
    private Animator animator;

    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1f;
    private float attackTimer = 0f;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = Player.Instance;

        animationData.Initialize();
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 5f)
        {
            if (distanceToPlayer <= attackRange)
            {
                AttackTarget();
            }
            else
            {
                MoveToTarget();
            }
        }
        else
            TransitionToIdle();
    }

    private void MoveToTarget()
    {
        agent.SetDestination(player.transform.position);
        StartAnimation(animationData.RunParameterHash);
        StopAnimation(animationData.IdleParameterHash);
    }

    private void AttackTarget()
    {
        agent.ResetPath();
        StopAnimation(animationData.RunParameterHash);

        if (attackTimer <= 0f)
        {
            attackTimer = attackCooldown;

            StartAnimation(animationData.AttackParameterHash);
        }
    }

    private void TransitionToIdle()
    {
        agent.ResetPath();
        StopAnimation(animationData.RunParameterHash);
        StartAnimation(animationData.IdleParameterHash);
    }

    public void ResetAttack()
    {
        StopAnimation(animationData.AttackParameterHash);
    }

    public void StartAnimation(int animatorHash)
    {
        animator.SetBool(animatorHash, true);
    }

    public void StopAnimation(int animatorHash)
    {
        animator.SetBool(animatorHash, false);
    }
}