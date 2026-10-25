using UnityEngine;

public class PumpkinSpawner2 : MonoBehaviour
{
    [SerializeField] private GameObject pumpkinPrefab;
    public int pumpkinCount = 20;
    public float areaSize = 10f;

    void Update()
    {
        while (GameObject.FindGameObjectsWithTag("Coin").Length < pumpkinCount)
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

