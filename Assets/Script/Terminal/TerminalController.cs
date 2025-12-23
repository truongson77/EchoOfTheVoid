using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Terminal : MonoBehaviour
{
    public DoorController door;

    private Animator animator;
    private bool activated = false;
    private PlayerController currentPlayer;

    [SerializeField] private Sprite activatedSprite;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        currentPlayer = player;
        player.SetCurrentTerminal(this);

        animator.SetBool("isPlayerNear", true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (activated) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        if (player == currentPlayer)
        {
            animator.SetBool("isPlayerNear", false);
            player.ClearCurrentTerminal(this);
            currentPlayer = null;
        }
    }

    public void Activate()
    {
        if (activated) return;
        if (currentPlayer == null) return;

        activated = true;

        animator.enabled = false; // dừng Animator
        GetComponent<SpriteRenderer>().sprite = activatedSprite;

        //animator.SetBool("isActivated", true);
        
        

        //animator.SetBool("isPlayerNear", false);

        GetComponent<Collider2D>().enabled = false;

        if (door != null)
            door.OpenDoor();
    }
}
