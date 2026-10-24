
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [Header("Vision")]
    [SerializeField] private bool enableVision = true;
    [SerializeField] private float visionRange = 10f;
    [SerializeField, Range(0f, 360f)] private float visionAngle = 90f;
    [SerializeField] private float eyeHeight = 1f;
    [SerializeField] private LayerMask obstacleMask;

    [Header("Close Range")]
    [SerializeField] private float closeDetectionRange = 1f;

    public Transform VisiblePlayer { get; private set; }
    public Transform VisibleCoin { get; private set; }

    public bool VisionEnabled => enableVision;

    public void Scan()
    {
        VisiblePlayer = null;
        VisibleCoin = null;

        if (!enableVision)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && CanSee(player.transform))
            VisiblePlayer = player.transform;

        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        float closestDistance = Mathf.Infinity;

        foreach (GameObject coin in coins)
        {
            if (!CanSee(coin.transform))
                continue;

            float distance = Vector3.Distance(
                transform.position, coin.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                VisibleCoin = coin.transform;
            }
        }
    }

    public bool CanSee(Transform target)
    {
        if (!enableVision || target == null)
            return false;

        Vector3 origin = transform.position + Vector3.up * eyeHeight;
        Vector3 destination = target.position + Vector3.up * 0.5f;
        Vector3 direction = destination - origin;

        if (direction.magnitude > visionRange)
            return false;

        // Allow close targets even outside the vision angle
        if (direction.magnitude > closeDetectionRange)
        {
            float angle = Vector3.Angle(transform.forward, direction);

            if (angle > visionAngle / 2f)
                return false;
        }

        // Walls can still block close targets
        if (Physics.Linecast(origin, destination, obstacleMask))
            return false;

        return true;
    }

    void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + Vector3.up * eyeHeight;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, visionRange);

        Vector3 left = Quaternion.Euler(
            0, -visionAngle / 2f, 0) * transform.forward;

        Vector3 right = Quaternion.Euler(
            0, visionAngle / 2f, 0) * transform.forward;

        Gizmos.DrawRay(origin, left * visionRange);
        Gizmos.DrawRay(origin, right * visionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(origin, closeDetectionRange);
    }
}
