using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class AutoDoor : MonoBehaviour
{
    private Animator animator;

    private bool isOpened = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Nếu đã mở rồi → không làm gì nữa
        if (isOpened) return;

        // Chỉ player mới mở được cửa
        if (other.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        isOpened = true;

        // Kích hoạt animation mở cửa
        animator.SetBool("isNear", true);

        // Tắt collider để không trigger lại
        GetComponent<Collider2D>().enabled = false;
    }
}
