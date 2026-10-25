
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    public enum EnemyState
    {
        Roam,
        Collect,
        Attack
    }
    
    [SerializeField] private EnemyState currentState;
    //
    private NavMeshAgent agent;
    private EnemyVision vision;
    private EnemyHearing hearing;
    //
    private EnemyRoam roam;
    private EnemyCollect collect;
    private EnemyAttack attack;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        vision = GetComponent<EnemyVision>();
        hearing = GetComponent<EnemyHearing>();
        //
        roam = GetComponent<EnemyRoam>();
        collect = GetComponent<EnemyCollect>();
        attack = GetComponent<EnemyAttack>();
        //
        currentState = EnemyState.Roam;
        roam.Enter();
    }

    void Update()
    {
        vision.Scan();
        hearing.Scan();

        //PRIORITY 1: PLAYER
        if (vision.VisiblePlayer != null || hearing.HeardPlayer != null)
        {
            Transform player = vision.VisiblePlayer != null
                ? vision.VisiblePlayer
                : hearing.HeardPlayer;

            attack.SetTarget(player);
            ChangeState(EnemyState.Attack);
        }
        //PRIORITY 2: COIN
        else if (vision.VisibleCoin != null)
        {
            collect.SetTarget(vision.VisibleCoin);
            ChangeState(EnemyState.Collect);
        }
        //PRIORITY 3: ROAM
        else
        {
            ChangeState(EnemyState.Roam);
        }

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
