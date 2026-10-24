
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float attackRange = 1.5f;

    private NavMeshAgent agent;
    private Transform targetPlayer;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Transform player)
    {
        targetPlayer = player;
    }

    public void Execute()
    {
        if (targetPlayer == null)
            return;

        float distance = Vector3.Distance(
            transform.position, targetPlayer.position);

        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(targetPlayer.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }
}
