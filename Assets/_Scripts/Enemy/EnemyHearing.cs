
using UnityEngine;

public class EnemyHearing : MonoBehaviour
{
    [Header("Hearing Settings")]
    [SerializeField] private bool enableHearing = true;
    [SerializeField] private float hearingRange = 5f;

    public Transform HeardPlayer { get; private set; }

    public void Scan()
    {
        HeardPlayer = null;

        if (!enableHearing)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.transform.position
        );

        if (distance <= hearingRange)
        {
            HeardPlayer = player.transform;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}
