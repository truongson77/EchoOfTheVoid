using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 1.2f;
    public float bulletSpeed = 8f;

    private Transform player;
    private float timer;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>().transform;
    }

    void Update()
    {
        if (player == null) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Shoot();
            timer = fireRate;
        }
    }

    void Shoot()
    {
        Vector2 dir = (player.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet b = bullet.GetComponent<Bullet>();
        b.Init(dir);
    }
}
