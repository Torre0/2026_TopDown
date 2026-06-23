using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;

    private Vector2 direction;

    public void SetTarget(Vector3 targetPosition)
    {
        direction =
            (targetPosition - transform.position).normalized;
    }

    private void Update()
    {
        transform.position +=
            (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth hp =
                other.GetComponent<PlayerHealth>();

            if (hp != null)
            {
                hp.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}