using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class MedikitController : MonoBehaviour
{
    private Animator animator;

    private bool playerInRange = false;
    private bool isOpened = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isOpened) return;

        // Player ở gần + nhấn E → mở medikit
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenMedikit();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened) return;

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            animator.SetBool("isNear", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (isOpened) return;

        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            animator.SetBool("isNear", false);
        }
    }

    void OpenMedikit()
    {
        isOpened = true;

        // Tắt highlight
        animator.SetBool("isNear", false);

        // Bắt đầu animation mở
        animator.SetBool("open", true);

        // Tắt trigger để không tương tác lại
        GetComponent<Collider2D>().enabled = false;
    }
}
