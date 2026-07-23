using UnityEngine;

public class Walking : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 3f;

    private bool movingToB = true;
    // Update is called once per frame
    void Update()
    {
        Transform target = movingToB ? pointB : pointA;

        // Move towards the target point
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // If we reach the target, switch direction
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            movingToB = !movingToB;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); //flips the object
        }

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(null);
        }
    }
}
