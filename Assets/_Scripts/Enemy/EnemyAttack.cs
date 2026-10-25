
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private GameObject player;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int attacksToKill = 5;
    [SerializeField] private float attackInterval = 1.5f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private NavMeshAgent agent;
    private Transform targetPlayer;

    private int attackCount = 0;
    private float nextAttackTime = 0f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Transform target)
    {
        targetPlayer = target;
    }

    public void Execute()
    {
        if (targetPlayer == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            targetPlayer.position
        );

        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(targetPlayer.position);
            return;
        }

        agent.isStopped = true;

        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger("Attack");

            attackCount++;

            nextAttackTime = Time.time + attackInterval;

            if (attackCount >= attacksToKill)
            {
                Destroy(player);
                targetPlayer = null;
            }
        }
    }
}
