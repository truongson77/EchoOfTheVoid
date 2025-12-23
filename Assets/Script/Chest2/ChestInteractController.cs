using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class ChestController : MonoBehaviour
{
    [Header("Opening Animation")]
    public Sprite[] openingFrames;
    public Sprite openedSprite;
    public float frameDelay = 0.08f;

    private SpriteRenderer sr;
    private Animator animator;
    private bool opened = false;
    private PlayerController currentPlayer;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (opened) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        currentPlayer = player;
        animator.SetBool("isHighlighted", true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (opened) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == currentPlayer)
        {
            animator.SetBool("isHighlighted", false);
            currentPlayer = null;
        }
    }

    public void OpenChest()
    {
        if (opened) return;
        if (currentPlayer == null) return;

        opened = true;

        animator.enabled = false;
        Debug.Log("OpenChest CALLED");
        
        StartCoroutine(OpenRoutine());
    }

    IEnumerator OpenRoutine()
    {
        // Play opening frames
        foreach (Sprite s in openingFrames)
        {
            sr.sprite = s;
            yield return new WaitForSeconds(frameDelay);
        }

        // Giữ frame cuối
        sr.sprite = openedSprite;

        // Không cho tương tác lại
        GetComponent<Collider2D>().enabled = false;
    }
}
