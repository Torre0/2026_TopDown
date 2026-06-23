using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("공격 위치")]
    public Transform attackPoint;

    [Header("공격 설정")]
    public float attackDistance = 0.8f;
    public float attackCooldown = 0.5f;

    [Header("적 레이어")]
    public LayerMask enemyLayer;

    [Header("공격 이펙트")]
    public GameObject attackEffectPrefab;

    private float nextAttackTime;

    private PlayerController playerController;
    private PlayerWeapon playerWeapon;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerWeapon = GetComponent<PlayerWeapon>();
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

        nextAttackTime =
            Time.time + playerWeapon.GetAttackSpeed();
    }

    private void Attack()
    {
        if (attackPoint == null)
        {
            Debug.LogError("AttackPoint가 연결되지 않았습니다!");
            return;
        }

        Vector2 dir = playerController.lookDirection;

        float angle =
            Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 공격 이펙트 생성
        if (attackEffectPrefab != null)
        {
            GameObject effect =
                Instantiate(
                    attackEffectPrefab,
                    attackPoint.position,
                    Quaternion.Euler(0, 0, angle));

            effect.transform.localScale =
                Vector3.one * playerWeapon.GetEffectScale();
        }

        Collider2D[] hitTargets =
            Physics2D.OverlapCircleAll(
                attackPoint.position,
                playerWeapon.GetAttackRange(),
                enemyLayer);

        foreach (Collider2D target in hitTargets)
        {
            Debug.Log("맞은 대상 : " + target.name);

            // 일반 몬스터
            EnemyHealth enemyHealth =
                target.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(
                    playerWeapon.GetDamage());

                Debug.Log(
                    "몬스터 데미지 : " +
                    playerWeapon.GetDamage());
            }

            // 보스
            BossHealth bossHealth =
                target.GetComponent<BossHealth>();

            if (bossHealth != null)
            {
                bossHealth.TakeDamage(
                    playerWeapon.GetDamage());

                Debug.Log(
                    "보스 데미지 : " +
                    playerWeapon.GetDamage());
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;

        float range = 1f;

        PlayerWeapon weapon =
            GetComponent<PlayerWeapon>();

        if (weapon != null)
        {
            range = weapon.GetAttackRange();
        }

        Gizmos.DrawWireSphere(
            attackPoint.position,
            range);
    }
}