using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;

    private int currentHealth;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("적 체력 : " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (anim != null)
        {
            anim.SetTrigger("Die");
        }

        Destroy(gameObject, 1f);
    }
}