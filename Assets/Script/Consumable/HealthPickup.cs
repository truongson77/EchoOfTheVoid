using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
