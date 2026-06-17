using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;

    private int currentHealth;
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
        currentHealth -= damage;

        Debug.Log("현재 체력 : " + currentHealth);

        StartCoroutine(HitEffect());

        if (currentHealth <= 0)
        {
            Die();
        }
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

        // 게임오버 처리
        // GameManager.Instance.GameOver();

        Destroy(gameObject);
    }
}