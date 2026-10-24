
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyVision))]
[RequireComponent(typeof(EnemyRoam))]
[RequireComponent(typeof(EnemyCollect))]
[RequireComponent(typeof(EnemyAttack))]

public class EnemyBrain : MonoBehaviour
{
    public enum EnemyState
    {
        Roam,
        Collect,
        Attack
    }

    [Header("Current State (Debug)")]
    [SerializeField] private EnemyState currentState;

    private NavMeshAgent agent;
    private EnemyVision vision;
    private EnemyRoam roam;
    private EnemyCollect collect;
    private EnemyAttack attack;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        vision = GetComponent<EnemyVision>();
        roam = GetComponent<EnemyRoam>();
        collect = GetComponent<EnemyCollect>();
        attack = GetComponent<EnemyAttack>();
    }

    void Start()
    {
        currentState = EnemyState.Roam;
        roam.Enter();
    }

    void Update()
    {
        // Update vision first
        vision.Scan();

        // PRIORITY 1: PLAYER
        if (vision.VisiblePlayer != null)
        {
            attack.SetTarget(vision.VisiblePlayer);
            ChangeState(EnemyState.Attack);
        }
        // PRIORITY 2: COIN
        else if (vision.VisibleCoin != null)
        {
            collect.SetTarget(vision.VisibleCoin);
            ChangeState(EnemyState.Collect);
        }
        // PRIORITY 3: ROAM
        else
        {
            ChangeState(EnemyState.Roam);
        }

        // Execute the active state
        switch (currentState)
        {
            case EnemyState.Roam:
                roam.Execute();
                break;

            case EnemyState.Collect:
                collect.Execute();
                break;

            case EnemyState.Attack:
                attack.Execute();
                break;
        }
    }

    void ChangeState(EnemyState newState)
    {
        if (currentState == newState)
            return;

        agent.isStopped = false;
        agent.ResetPath();

        currentState = newState;

        if (currentState == EnemyState.Roam)
            roam.Enter();
    }
}
