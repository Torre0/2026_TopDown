using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPBarUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image hpFill;

    public TMP_Text hpText;

    private void Update()
    {
        if (playerHealth == null)
            return;

        int currentHp = playerHealth.GetCurrentHealth();
        int maxHp = playerHealth.GetMaxHealth();

        hpFill.fillAmount =
            (float)currentHp / maxHp;

        hpText.text =
            currentHp + " / " + maxHp;
    }
}