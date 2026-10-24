
using UnityEngine;
using UnityEngine.AI;

public class EnemyRoam : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float stoppingDistance = 0.5f;

    private NavMeshAgent agent;
    private int currentPoint = -1;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Enter()
    {
        PickRandomPoint();
    }

    public void Execute()
    {
        if (patrolPoints.Length == 0)
            return;

        if (!agent.pathPending &&
            agent.hasPath &&
            agent.remainingDistance <= stoppingDistance)
        {
            PickRandomPoint();
        }
    }

    void PickRandomPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        int randomPoint = Random.Range(0, patrolPoints.Length);

        if (patrolPoints.Length > 1)
        {
            while (randomPoint == currentPoint)
            {
                randomPoint = Random.Range(0, patrolPoints.Length);
            }
        }

        currentPoint = randomPoint;

        if (patrolPoints[currentPoint] != null)
            agent.SetDestination(patrolPoints[currentPoint].position);
    }
}
