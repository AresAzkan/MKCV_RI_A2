
using UnityEngine;
using UnityEngine.AI;

public class EnemyCollect : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform targetCoin;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Transform coin)
    {
        targetCoin = coin;
    }

    public void Execute()
    {
        if (targetCoin == null)
            return;

        agent.SetDestination(targetCoin.position);
    }
}
