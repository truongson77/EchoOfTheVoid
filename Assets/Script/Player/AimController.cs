using UnityEngine;

public class AimController : MonoBehaviour
{
    public Camera mainCamera;
    public CrosshairUI crosshair;

    public Vector2 AimDirection { get; private set; }
    public Vector2 AimWorldPosition { get; private set; }

    void Update()
    {
        UpdateAim();
    }

    void UpdateAim()
    {
        Vector2 screenPos = crosshair.GetScreenPosition();
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;

        AimWorldPosition = worldPos;

        Vector2 dir = worldPos - transform.position;
        AimDirection = dir.normalized;
    }
}
