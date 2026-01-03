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

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private bool isGrounded = true;

    private Terminal currentTerminal;
    private ChestController currentChest;
    private PlayerHealth playerHealth;
    public AimController aimController;

    // ================= AUDIO =================
    [Header("Audio")]
    public AudioSource footstepSource;
    public AudioClip footstepClip;

    private bool isPlayingFootstep;

    // ================= AIM FLIP =================
    void HandleAimFlip()
    {
        if (aimController == null) return;

        float aimX = aimController.AimDirection.x;

        if (aimX >= 0.01f)
            spriteRenderer.flipX = false;
        else if (aimX <= -0.01f)
            spriteRenderer.flipX = true;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb.gravityScale = 0f;      // Top-down
        rb.freezeRotation = true;

        playerHealth = GetComponent<PlayerHealth>();
        playerHealth.OnPlayerDied += OnPlayerDied;
    }

    void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnPlayerDied -= OnPlayerDied;
    }

    // ================= DEATH =================
    void OnPlayerDied()
    {
        rb.linearVelocity = Vector2.zero;

        animator.SetBool("isDead", true);
        animator.SetBool("isMoving", false);

        StopFootstep();
    }

    // ================= MOVEMENT =================
    void HandleMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        bool isMoving = movement.sqrMagnitude > 0.01f;
        animator.SetBool("isMoving", isMoving);

        HandleFootstepSound(isMoving);
    }

    void Update()
    {
        if (playerHealth.IsDead) return;

        HandleMovement();
        HandleAimFlip();

        // ===== JUMP (SPACE) =====
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // ===== INTERACT TERMINAL (E) =====
        if (Input.GetKeyDown(KeyCode.E) && currentTerminal != null)
        {
            currentTerminal.Activate();
        }

        // ===== OPEN CHEST (E) =====
        if (Input.GetKeyDown(KeyCode.E) && currentChest != null)
        {
            currentChest.OpenChest();
        }
    }

    void FixedUpdate()
    {
        if (playerHealth.IsDead) return;

        if (movement.sqrMagnitude > 0.01f)
            rb.linearVelocity = movement * moveSpeed;
        else
            rb.linearVelocity = Vector2.zero;
    }

    // ================= FOOTSTEP AUDIO =================
    void HandleFootstepSound(bool isMoving)
    {
        if (footstepSource == null || footstepClip == null) return;

        if (isMoving && !isPlayingFootstep)
        {
            footstepSource.clip = footstepClip;
            footstepSource.loop = true;
            footstepSource.Play();
            isPlayingFootstep = true;
        }
        else if (!isMoving && isPlayingFootstep)
        {
            StopFootstep();
        }
    }

    void StopFootstep()
    {
        if (footstepSource == null) return;

        footstepSource.Stop();
        isPlayingFootstep = false;
    }

    // ================= TRIGGERS =================
    void OnTriggerEnter2D(Collider2D other)
    {
        ChestController chest = other.GetComponent<ChestController>();
        if (chest != null)
            currentChest = chest;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ChestController chest = other.GetComponent<ChestController>();
        if (chest != null && chest == currentChest)
            currentChest = null;
    }

    public void SetCurrentChest(ChestController chest)
    {
        currentChest = chest;
    }

    public void ClearCurrentChest(ChestController chest)
    {
        if (currentChest == chest)
            currentChest = null;
    }

    public void SetCurrentTerminal(Terminal terminal)
    {
        currentTerminal = terminal;
    }

    public void ClearCurrentTerminal(Terminal terminal)
    {
        if (currentTerminal == terminal)
            currentTerminal = null;
    }

    // ================= JUMP =================
    void Jump()
    {
        animator.SetBool("isJumping", true);
        isGrounded = false;

        Invoke(nameof(EndJump), 0.4f);
    }

    void EndJump()
    {
        animator.SetBool("isJumping", false);
        isGrounded = true;
    }
}
