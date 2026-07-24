using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Offset")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 1.5f, -10f);

    [Header("Smoothing")]
    [SerializeField] private float smoothTime = 0.2f;

    [Header("Look Ahead")]
    [SerializeField] private float lookAheadDistance = 2f;
    [SerializeField] private float lookAheadSpeed = 4f;

    [Header("Dead Zone")]
    [SerializeField] private Vector2 deadZoneSize = new Vector2(2f, 1.5f);

    [Header("Camera Bounds")]
    [SerializeField] private bool useBounds = false;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    private Vector3 velocity;
    private float currentLookAhead;
    private Vector3 focusPoint;

    private void Start()
    {
        if (target != null)
            focusPoint = target.position;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // ---------- Dead Zone ----------
        Vector3 difference = target.position - focusPoint;

        if (Mathf.Abs(difference.x) > deadZoneSize.x / 2f)
            focusPoint.x = target.position.x - Mathf.Sign(difference.x) * deadZoneSize.x / 2f;

        if (Mathf.Abs(difference.y) > deadZoneSize.y / 2f)
            focusPoint.y = target.position.y - Mathf.Sign(difference.y) * deadZoneSize.y / 2f;

        // ---------- Look Ahead ----------
        float targetLookAhead = 0f;

        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
            {
                targetLookAhead = Mathf.Sign(rb.linearVelocity.x) * lookAheadDistance;
            }
        }

        currentLookAhead = Mathf.Lerp(
            currentLookAhead,
            targetLookAhead,
            lookAheadSpeed * Time.deltaTime
        );

        Vector3 desiredPosition = focusPoint + offset;
        desiredPosition.x += currentLookAhead;

        // ---------- Bounds ----------
        if (useBounds)
        {
            Camera cam = GetComponent<Camera>();

            float halfHeight = cam.orthographicSize;
            float halfWidth = halfHeight * cam.aspect;

            desiredPosition.x = Mathf.Clamp(
                desiredPosition.x,
                minBounds.x + halfWidth,
                maxBounds.x - halfWidth);

            desiredPosition.y = Mathf.Clamp(
                desiredPosition.y,
                minBounds.y + halfHeight,
                maxBounds.y - halfHeight);
        }

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector3 centre = Application.isPlaying ? focusPoint : transform.position;

        Gizmos.DrawWireCube(
            centre,
            new Vector3(deadZoneSize.x, deadZoneSize.y, 0f)
        );
    }
}
