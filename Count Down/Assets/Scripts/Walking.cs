using UnityEngine;

public class Walking : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float pauseDuration = 0.4f;

    public Vector2 DeltaMovement { get; private set; }

    private Vector3 lastPosition;
    private bool movingToB = true;
    private float journeyLength;
    private float distanceTraveled;
    private float pauseTimer;

    private Vector3 segmentStart;
    private Vector3 segmentTarget;

    private Vector3 initialPosition;
    private Vector3 initialScale;
    private bool initialMovingToB;

    void Awake()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;
        initialMovingToB = movingToB;
    }

    void OnEnable()
    {
        transform.position = initialPosition;
        transform.localScale = initialScale;
        movingToB = initialMovingToB;
        lastPosition = initialPosition;
        DeltaMovement = Vector2.zero;
        distanceTraveled = 0f;
        pauseTimer = 0f;

        // First leg starts from wherever the platform actually spawned,
        // not necessarily from pointA or pointB
        segmentStart = initialPosition;
        segmentTarget = movingToB ? pointB.position : pointA.position;
        journeyLength = Vector3.Distance(segmentStart, segmentTarget);
    }

    void FixedUpdate()
    {
        if (pauseTimer > 0f)
        {
            pauseTimer -= Time.fixedDeltaTime;
            DeltaMovement = Vector2.zero;
            lastPosition = transform.position;
            return;
        }

        // Guard against a zero-length segment (e.g. spawned exactly on the target)
        if (journeyLength <= 0.0001f)
        {
            AdvanceToNextSegment();
        }

        distanceTraveled += speed * Time.fixedDeltaTime;
        float rawT = Mathf.Clamp01(distanceTraveled / journeyLength);

        float easedT = Mathf.SmoothStep(0f, 1f, rawT);
        transform.position = Vector3.Lerp(segmentStart, segmentTarget, easedT);

        if (rawT >= 1f)
        {
            AdvanceToNextSegment();
        }

        DeltaMovement = (Vector2)(transform.position - lastPosition);
        lastPosition = transform.position;
    }

    void AdvanceToNextSegment()
    {
        movingToB = !movingToB;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        // From here on, segments always run cleanly between pointA and pointB
        segmentStart = movingToB ? pointA.position : pointB.position;
        segmentTarget = movingToB ? pointB.position : pointA.position;
        journeyLength = Vector3.Distance(segmentStart, segmentTarget);

        distanceTraveled = 0f;
        pauseTimer = pauseDuration;
    }
}