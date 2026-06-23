using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("공격 설정")]
    public float attackInterval = 2f;
    public float attackRange = 5f;

    private float timer;
    private Transform player;

    private void Start()
    {
        GameObject obj =
            GameObject.FindGameObjectWithTag("Player");

        if (obj != null)
            player = obj.transform;
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance =
            Vector2.Distance(
                transform.position,
                player.position);

        // 공격 범위 밖이면 공격 안함
        if (distance > attackRange)
            return;

        timer += Time.deltaTime;

        if (timer >= attackInterval)
        {
            timer = 0f;
            Attack();
        }
    }

    private void Attack()
    {
        GameObject projectile =
            Instantiate(
                projectilePrefab,
                firePoint.position,
                Quaternion.identity);

        BossProjectile bossProjectile =
            projectile.GetComponent<BossProjectile>();

        if (bossProjectile != null)
        {
            bossProjectile.SetTarget(
                player.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            attackRange);
    }
}