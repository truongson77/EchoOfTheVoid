using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Jump")]
    public float jumpForce = 5f;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    Vector2 lookDirection = new Vector2(1, 0); // player facing rightward initially


    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f;


    private Vector2 movement;
    private bool isGrounded = true;
    private bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb.gravityScale = 0f;   // Top-down: không trọng lực
        rb.freezeRotation = true;

        currentHealth = 50;
    }

    void Update()
    {
        if (isDead) return;

        // ===== INPUT DI CHUYỂN =====
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        if (movement.sqrMagnitude > 0.01f)
        {
            lookDirection = movement;
        }

        bool isMoving = movement.sqrMagnitude > 0.01f;
        animator.SetBool("isMoving", isMoving);

        // ===== FLIP SPRITE =====
        if (movement.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (movement.x < -0.01f)
            spriteRenderer.flipX = true;

        // ===== NHẢY (PHÍM SPACE) =====
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // ===== SHOOT (LEFT MOUSE BUTTON) =====
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Player shooting in direction: " + lookDirection);
            Shoot(lookDirection);
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        if (movement.sqrMagnitude > 0.01f)
            rb.linearVelocity = movement * moveSpeed;
        else
            rb.linearVelocity = Vector2.zero;
    }

    // ================== JUMP ==================
    void Jump()
    {
        animator.SetBool("isJumping", true);
        isGrounded = false;

        // Top-down: nhảy chỉ mang tính animation
        Invoke(nameof(EndJump), 0.4f); // thời gian animation jump
    }

    void EndJump()
    {
        animator.SetBool("isJumping", false);
        isGrounded = true;
    }

    // =================== SHOOT ==================
    void Shoot(Vector2 direction)
    {
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BulletsEnergyBlueController bulletScript = bullet.GetComponent<BulletsEnergyBlueController>();
            if (bulletScript != null)
            {
                bulletScript.Shoot(direction, bulletSpeed);
            }
        }
    }

    // ================== HEALTH ==================
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Took Damage: " + damage + ", Current Health: " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Healed: " + amount + ", Current Health: " + currentHealth);
    }

    void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;

        animator.SetBool("isDead", true);
        animator.SetBool("isMoving", false);
    }
}
