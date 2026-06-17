using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;

    public float attackDistance = 0.8f;
    public float attackRange = 0.8f;
    public int attackDamage = 10;
    public float attackCooldown = 0.5f;

    public LayerMask enemyLayer;

    [Header("공격 이펙트")]
    public GameObject attackEffectPrefab;

    private float nextAttackTime;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        UpdateAttackPoint();
    }

    private void UpdateAttackPoint()
    {
        if (attackPoint == null || playerController == null)
            return;

        attackPoint.localPosition =
            playerController.lookDirection * attackDistance;
    }

    private void OnAttack()
    {
        if (Time.time < nextAttackTime)
            return;

        Attack();

        nextAttackTime = Time.time + attackCooldown;
    }

    private void Attack()
    {
        if (attackPoint == null)
        {
            Debug.LogError("AttackPoint가 연결되지 않았습니다!");
            return;
        }

        Debug.Log("공격!");

        Vector2 dir = playerController.lookDirection;

        float angle =
            Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (attackEffectPrefab != null)
        {
            Instantiate(
                attackEffectPrefab,
                attackPoint.position,
                Quaternion.Euler(0, 0, angle));
        }

        Collider2D[] hitEnemies =
            Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRange,
                enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth =
                enemy.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            attackPoint.position,
            attackRange);
    }
}