using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image fillImage;
    public Image chipImage;
    public float chipSpeed = 2f;

    private IHealth healthSource;
    private float targetFill;

    void Awake()
    {
        healthSource = GetComponentInParent<IHealth>();

        if (healthSource == null)
        {
            PlayerHealth player = FindFirstObjectByType<PlayerHealth>();
            if (player != null)
                healthSource = player;
        }

        if (healthSource == null)
        {
            Debug.LogError("[HealthBarUI] Không tìm thấy Health Source");
            enabled = false;
            return;
        }

        healthSource.OnHealthChanged += OnHealthChanged;
        OnHealthChanged(
            healthSource.CurrentHealth,
            healthSource.MaxHealth
        );
    }

    void OnDestroy()
    {
        if (healthSource != null)
            healthSource.OnHealthChanged -= OnHealthChanged;
    }

    void OnHealthChanged(int current, int max)
    {
        targetFill = (float)current / max;
        fillImage.fillAmount = targetFill;
    }

    void Update()
    {
        if (chipImage != null && chipImage.fillAmount > targetFill)
        {
            chipImage.fillAmount = Mathf.Lerp(
                chipImage.fillAmount,
                targetFill,
                Time.deltaTime * chipSpeed
            );
        }
    }
}
