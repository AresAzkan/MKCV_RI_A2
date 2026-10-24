
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    enum EnemyState
    {
        Patrol,
        Collect
    }

    [Header("Patrol Settings")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float stoppingDistance = 0.5f;

    [Header("Vision Settings")]
    [SerializeField] private bool enableVision = true;
    [SerializeField] private float visionRange = 10f;
    [SerializeField, Range(0f, 360f)] private float visionAngle = 90f;
    [SerializeField] private LayerMask obstacleMask;

    private NavMeshAgent agent;
    private EnemyState currentState = EnemyState.Patrol;

    private int currentPoint = -1;
    private Transform targetCoin;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        PickRandomPoint();
    }

    void Update()
    {
        // Check for coins
        if (enableVision)
        {
            FindVisibleCoin();
        }

        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Collect:
                Collect();
                break;
        }
    }

    void Patrol()
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
        {
            agent.SetDestination(patrolPoints[currentPoint].position);
        }
    }

    void FindVisibleCoin()
    {
        if (targetCoin != null)
            return;

        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        foreach (GameObject coin in coins)
        {
            Vector3 direction = coin.transform.position - transform.position;

            if (direction.magnitude > visionRange)
                continue;

            float angle = Vector3.Angle(transform.forward, direction);

            if (angle > visionAngle / 2f)
                continue;

            // Check whether a wall blocks the view
            if (Physics.Linecast(
                transform.position + Vector3.up,
                coin.transform.position + Vector3.up * 0.5f,
                obstacleMask))
            {
                continue;
            }

            targetCoin = coin.transform;
            currentState = EnemyState.Collect;
            agent.SetDestination(targetCoin.position);
            return;
        }
    }

    void Collect()
    {
        if (targetCoin == null)
        {
            targetCoin = null;
            currentState = EnemyState.Patrol;
            PickRandomPoint();
            return;
        }

        agent.SetDestination(targetCoin.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Vector3 leftDirection = Quaternion.Euler(
            0, -visionAngle / 2f, 0) * transform.forward;

        Vector3 rightDirection = Quaternion.Euler(
            0, visionAngle / 2f, 0) * transform.forward;

        Gizmos.DrawRay(transform.position, leftDirection * visionRange);
        Gizmos.DrawRay(transform.position, rightDirection * visionRange);
    }
}
