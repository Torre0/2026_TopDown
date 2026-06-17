using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("공격 설정")]
    public Transform attackPoint;
    public float attackRange = 0.8f;
    public int attackDamage = 10;
    public float attackCooldown = 0.5f;

    [Header("적 레이어")]
    public LayerMask enemyLayer;

    private float nextAttackTime;

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (Time.time < nextAttackTime)
            return;

        Attack();

        nextAttackTime = Time.time + attackCooldown;
    }

    private void Attack()
    {
        Debug.Log("공격!");

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