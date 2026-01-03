using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 10;
    public bool destroyOnHit = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerHealth player))
        {
            player.TakeDamage(damage);
            if (destroyOnHit) Destroy(gameObject);
        }
        else if (other.TryGetComponent(out EnemyHealth enemy))
        {
            enemy.TakeDamage(damage);
            if (destroyOnHit) Destroy(gameObject);
        }
    }
}
