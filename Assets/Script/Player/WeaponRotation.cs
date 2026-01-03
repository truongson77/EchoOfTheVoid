using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [Header("References")]
    public AimController aimController;
    public SpriteRenderer gunSprite;

    [Header("Weapon Offset")]
    public Vector2 rightHandOffset = new Vector2(0.25f, -0.05f);

    private Vector2 leftHandOffset;

    void Awake()
    {
        // Tự động tạo offset tay trái
        leftHandOffset = new Vector2(-rightHandOffset.x, rightHandOffset.y);
    }

    void Update()
    {
        if (aimController == null) return;

        HandleRotation();
        HandlePosition();
    }

    void HandleRotation()
    {
        Vector2 dir = aimController.AimDirection;
        if (dir.sqrMagnitude < 0.01f) return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Flip sprite súng khi aim trái
        gunSprite.flipY = angle > 90f || angle < -90f;
    }

    void HandlePosition()
    {
        float aimX = aimController.AimDirection.x;

        // Aim phải → tay phải
        if (aimX >= 0.01f)
        {
            transform.localPosition = rightHandOffset;
        }
        // Aim trái → tay trái
        else if (aimX <= -0.01f)
        {
            transform.localPosition = leftHandOffset;
        }
    }
}
