using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float stopDistance = 4f;
    public float retreatDistance = 2f;

    private Transform player;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerHealth>().transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 dir = (player.position - transform.position).normalized;

        if (distance > stopDistance)
        {
            rb.linearVelocity = dir * moveSpeed;          // Tiến lên
        }
        else if (distance < retreatDistance)
        {
            rb.linearVelocity = -dir * moveSpeed;         // Lùi lại
        }
        else
        {
            rb.linearVelocity = Vector2.zero;             // Đứng bắn
        }
    }
}
