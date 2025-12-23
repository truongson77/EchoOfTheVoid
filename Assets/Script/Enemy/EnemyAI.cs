using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f;
    public float changeTime = 1f;
    private Animator animator;
    private Rigidbody2D rb;
    private float timer;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChooseNewDirection();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeTime)
        {
            ChooseNewDirection();
            timer = 0f;
        }
    }

    void FixedUpdate()
    {
        // wondering around
        Vector2 position = rb.position;
        position += moveDirection * speed * Time.fixedDeltaTime;
        rb.MovePosition(position);

        // animation logic
        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            if (animator != null)
            {
                animator.SetBool("isMoving", true);
            }
            if (moveDirection.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (moveDirection.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    void ChooseNewDirection()
    {
        // Choose a random direction (including staying still occasionally)
        float randomX = Random.Range(-0.3f, 0.3f);
        float randomY = Random.Range(-0.3f, 0.3f);

        moveDirection = new Vector2(randomX, randomY).normalized;

        // 60% chance to stop moving
        if (Random.value < 0.6f)
        {
            moveDirection = Vector2.zero;
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }
        }
    }
}
