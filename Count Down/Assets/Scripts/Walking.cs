using UnityEngine;

public class Walking : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 3f;
    public Vector2 DeltaMovement { get; private set; }
    private Vector3 lastPosition;
    private bool movingToB = true;
    // Update is called once per frame
    void FixedUpdate()
    {
        Transform target = movingToB ? pointB : pointA;

        // Move towards the target point
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.fixedDeltaTime
        );

        // If we reach the target, switch direction
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            movingToB = !movingToB;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); //flips the object
        }
        
        DeltaMovement = (Vector2)(transform.position - lastPosition);
        lastPosition = transform.position;

    }
    void Start()
    {
        lastPosition = transform.position;
    }
}
