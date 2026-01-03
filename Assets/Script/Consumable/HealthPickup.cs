using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthPickup : MonoBehaviour
{
    [Header("Heal Settings")]
    public int healAmount = 25;
    public bool destroyOnPickup = true;

    private void Reset()
    {
        // Đảm bảo collider là trigger
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Chỉ Player mới được nhặt
        if (!other.CompareTag("Player")) return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth == null) return;

        // Nếu player đã chết thì không cho nhặt
        if (playerHealth.IsDead) return;

        playerHealth.Heal(healAmount);

        OnPickedUp();

        if (destroyOnPickup)
            Destroy(gameObject);
    }

    protected virtual void OnPickedUp()
    {
        // Hook cho animation / sound sau này
        // Ví dụ: play sound, effect, popup text
    }
}
