using UnityEngine;

public class PumpkinSpawner : MonoBehaviour
{
    public GameObject pumpkinPrefab;
    public int pumpkinCount = 20;
    public float areaSize = 10f;

    void Update()
    {
        while (GameObject.FindGameObjectsWithTag("Pumpkin").Length < pumpkinCount)
        {
            Vector3 pos = new Vector3(
                Random.Range(-areaSize, areaSize),
                0.5f,
                Random.Range(-areaSize, areaSize)
            );

            Instantiate(pumpkinPrefab, pos, Quaternion.identity);
        }
    }
}

