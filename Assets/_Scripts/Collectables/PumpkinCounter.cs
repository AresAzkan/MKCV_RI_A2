using UnityEngine;
using TMPro;

public class PumpkinCounter : MonoBehaviour
{
    public int pumpkinsCollected = 0;
    public TMP_Text counterText;

    void Start()
    {
        UpdateText();
    }

    public void CollectPumpkin()
    {
        pumpkinsCollected++;
        UpdateText();
    }

    void UpdateText()
    {
        counterText.text = "Pumpkins Collected: " + pumpkinsCollected;
    }
}

