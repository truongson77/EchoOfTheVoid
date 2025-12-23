using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class DoorController : MonoBehaviour
{
    private Animator animator;
    private Collider2D doorCollider;
    private bool opened = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
    }

    public void OpenDoor()
    {
        if (opened) return;

        opened = true;

        animator.SetBool("isOpen", true);

        // Tắt collider để đi xuyên
        doorCollider.enabled = false;
    }
}
