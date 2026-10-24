using UnityEngine;

public class PumpkinCollect : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindAnyObjectByType<PumpkinCounter>().CollectPumpkin();
            Destroy(gameObject);
        }
    }
}

