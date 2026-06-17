using UnityEngine;

public class EnemyTraceController : MonoBehaviour
{
    [HideInInspector]
    public bool isKnockback = false;

    [Header("이동 설정")]
    public float moveSpeed = 2f;
    public float traceDistance = 5f;

    [Header("공격 설정")]
    public int damage = 10;
    public float attackDistance = 1f;
    public float attackCooldown = 1f;

    private Transform player;
    private Rigidbody2D rb;

    private float lastAttackTime;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // 넉백 중에는 움직이지 않음
        if (isKnockback)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float distance =
            Vector2.Distance(transform.position, player.position);

        // 추적 범위 밖
        if (distance > traceDistance)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // 공격 범위 안
        if (distance <= attackDistance)
        {
            rb.linearVelocity = Vector2.zero;

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }

            return;
        }

        // 플레이어 추적
        Vector2 direction =
            (player.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }

    private void AttackPlayer()
    {
        PlayerHealth playerHealth =
            player.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        Debug.Log($"{gameObject.name} 공격!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, traceDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}