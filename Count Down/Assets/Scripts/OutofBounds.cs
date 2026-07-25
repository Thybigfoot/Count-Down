using UnityEngine;

public class OutofBounds : MonoBehaviour
{
    
    [SerializeField] private Transform pointA;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = pointA.position;
        }
    }
}
