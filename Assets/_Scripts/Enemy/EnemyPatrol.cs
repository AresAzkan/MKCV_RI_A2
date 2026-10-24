
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float stoppingDistance = 0.5f;

    private NavMeshAgent agent;
    private int currentPoint = -1;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        PickRandomPoint();
    }

    void Update()
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

        // Avoid picking the same point twice
        if (patrolPoints.Length > 1)
        {
            while (randomPoint == currentPoint)
            {
                randomPoint = Random.Range(0, patrolPoints.Length);
            }
        }

        currentPoint = randomPoint;

        if (patrolPoints[currentPoint] != null)
        {
            agent.SetDestination(patrolPoints[currentPoint].position);
        }
    }
}
