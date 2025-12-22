using UnityEngine;

public class ChestInteractController : MonoBehaviour
{
    public Animator animator;
    private bool isInInteractionRange = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with chest");
        isInInteractionRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInInteractionRange = false;
        Debug.Log("Exit interaction range");
    }
    private void Update()
    {
        if (isInInteractionRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacting with chest");
            if (animator != null) {
                animator.SetBool("isOpened", true);
            }
        }
    }
}
