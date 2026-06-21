using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    private void Update()
    {
        goldText.text =
            "" +
            GameDataManager.Instance.saveData.totalGold;
    }
}