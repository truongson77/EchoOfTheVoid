using UnityEngine;

public class CrosshairUI : MonoBehaviour
{
    [Header("Settings")]
    public float followSpeed = 25f;
    public float maxDistanceFromCenter = 400f;

    private RectTransform rectTransform;
    private Vector2 targetPos;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Vector2 offset = mousePos - screenCenter;

        // Giới hạn crosshair không đi quá xa (controller-friendly)
        offset = Vector2.ClampMagnitude(offset, maxDistanceFromCenter);

        targetPos = screenCenter + offset;

        // Smooth movement
        rectTransform.position = Vector2.Lerp(
            rectTransform.position,
            targetPos,
            Time.deltaTime * followSpeed
        );
    }

    public Vector2 GetScreenPosition()
    {
        return rectTransform.position;
    }
}
