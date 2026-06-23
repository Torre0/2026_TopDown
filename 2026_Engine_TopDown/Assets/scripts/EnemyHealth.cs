using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("체력")]
    public int maxHealth = 30;

    [Header("넉백")]
    public float knockbackDistance = 1f;
    public float knockbackDuration = 0.15f;

    [Header("코인 드랍")]
    public GameObject coinPrefab;
    public int minCoinDrop = 1;
    public int maxCoinDrop = 5;

    private int currentHealth;
    private bool isDead;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private EnemyTraceController trace;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        trace = GetComponent<EnemyTraceController>();
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

        Debug.Log($"{gameObject.name} 체력 : {currentHealth}");

        StartCoroutine(HitEffect());

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(Knockback());
    }

    private IEnumerator HitEffect()
    {
        if (sr == null)
            yield break;

        sr.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        sr.color = Color.white;
    }

    private IEnumerator Knockback()
    {
        if (trace != null)
            trace.isKnockback = true;

        GameObject player =
            GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector2 direction =
                (transform.position - player.transform.position).normalized;

            transform.position +=
                (Vector3)(direction * knockbackDistance);
        }

        yield return new WaitForSeconds(knockbackDuration);

        if (trace != null)
            trace.isKnockback = false;
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        Vector3 deathPosition = transform.position;

        int dropCount =
            Random.Range(
                minCoinDrop,
                maxCoinDrop + 1);

        for (int i = 0; i < dropCount; i++)
        {
            GameObject coin =
                Instantiate(
                    coinPrefab,
                    transform.position,
                    Quaternion.identity);

            Rigidbody2D coinRb =
                coin.GetComponent<Rigidbody2D>();

            if (coinRb != null)
            {
                Vector2 direction =
                    Random.insideUnitCircle.normalized;

                float force =
                    Random.Range(1f, 2f);

                coinRb.AddForce(
                    direction * force,
                    ForceMode2D.Impulse);
            }
        }

        if (trace != null)
            trace.enabled = false;

        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        if (StageManager.Instance != null)
        {
            StageManager.Instance.SetLastEnemyPosition(
                transform.position);
        }

        Destroy(gameObject, 0.2f);
    }
}