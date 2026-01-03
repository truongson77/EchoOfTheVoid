using UnityEngine;
using System;

public class GunShooter : MonoBehaviour
{
    [Header("References")]
    public AimController aimController;
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Ammo")]
    public int maxAmmo = 30;
    public int currentAmmo;

    [Header("Fire Settings")]
    public float fireRate = 0.2f;

    public event Action<int, int> OnAmmoChanged;

    private float fireTimer;

    // ================= AUDIO =================
    [Header("Audio")]
    public AudioSource gunAudio;
    public AudioClip shootClip;
    [Range(0.8f, 1.2f)]
    public float pitchRandomMin = 0.95f;
    [Range(0.8f, 1.2f)]
    public float pitchRandomMax = 1.05f;

    void Awake()
    {
        currentAmmo = maxAmmo;
        OnAmmoChanged?.Invoke(currentAmmo, maxAmmo);
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        if (fireTimer > 0f) return;
        if (currentAmmo <= 0) return;

        Shoot();
        currentAmmo--;

        OnAmmoChanged?.Invoke(currentAmmo, maxAmmo);
        fireTimer = fireRate;
    }

    void Shoot()
    {
        // ===== SPAWN BULLET =====
        GameObject bulletObj = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.Init(aimController.AimDirection);

        // ===== PLAY SHOOT SOUND =====
        PlayShootSound();
    }

    void PlayShootSound()
    {
        if (gunAudio == null || shootClip == null) return;

        gunAudio.pitch = UnityEngine.Random.Range(pitchRandomMin, pitchRandomMax);
        gunAudio.PlayOneShot(shootClip);
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
        OnAmmoChanged?.Invoke(currentAmmo, maxAmmo);
    }
}
