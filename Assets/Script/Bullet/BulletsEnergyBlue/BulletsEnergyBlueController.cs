using UnityEngine;

public class BulletsEnergyBlueController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float range = 8f; // max range before auto-destroy
    private Vector2 startPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        float distanceTraveled = Vector2.Distance(transform.position, startPosition);

        if (distanceTraveled > range)
        {
            Destroy(gameObject);
        }
    }


    public void Shoot(Vector2 direction, float speed)
    {
        startPosition = transform.position;
        rb.linearVelocity = direction.normalized * speed;

        // Rotate bullet to face the direction (assuming sprite faces right by default)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet hit: " + collision.gameObject.name);
        rb.linearVelocity = Vector2.zero; // stop movement

        // Deal damage
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealthController enemyHealth = collision.gameObject.GetComponent<EnemyHealthController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(20f); // deal 20 damage
            }
        }
        Destroy(gameObject);
    }
}
