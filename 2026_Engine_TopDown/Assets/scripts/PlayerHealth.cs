using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("게임오버 UI")]
    public GameObject gameOverPanel;

    public int maxHealth = 100;

    private int currentHealth;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        maxHealth = GameDataManager.Instance.GetPlayerHp();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("현재 체력 : " + currentHealth);

        StartCoroutine(HitEffect());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    private IEnumerator HitEffect()
    {
        if (sr == null)
            yield break;

        sr.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        sr.color = Color.white;
    }

    private void Die()
{
    Debug.Log("플레이어 사망");

    if (gameOverPanel != null)
    {
        gameOverPanel.SetActive(true);
    }

    Time.timeScale = 0f;
}
}