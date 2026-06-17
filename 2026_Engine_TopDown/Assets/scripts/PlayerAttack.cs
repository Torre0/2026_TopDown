using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.8f;
    public int attackDamage = 10;
    public float attackCooldown = 0.5f;

    public LayerMask enemyLayer;

    private float nextAttackTime;

    // Send Messages 방식
    private void OnAttack()
    {
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
}