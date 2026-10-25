
using UnityEngine;

public class PumpkinCollector : MonoBehaviour

{
    public int PlayerScore = 0;
    public int EnemyScore = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyScore++;
            Destroy(gameObject);
        }
        
        if (other.CompareTag("Player"))
        {
            PlayerScore++;
            Destroy(gameObject);
        }
    }
}
