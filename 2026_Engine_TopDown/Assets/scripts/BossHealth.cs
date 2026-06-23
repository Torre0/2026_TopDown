using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 300;

    private int currentHealth;
    private bool isDead;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        Debug.Log("보스 체력 : " + currentHealth);

        if (sr != null)
        {
            sr.color = Color.red;
            Invoke(nameof(ResetColor), 0.1f);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ResetColor()
    {
        if (sr != null)
            sr.color = Color.white;
    }

    void Die()
    {
        isDead = true;

        if (StageManager.Instance != null)
        {
            StageManager.Instance.SetLastEnemyPosition(
                transform.position);
        }

        Debug.Log("보스 처치!");

        Destroy(gameObject);
    }
}