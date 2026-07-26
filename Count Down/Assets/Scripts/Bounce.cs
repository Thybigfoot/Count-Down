using UnityEngine;
using TarodevController;

public class MushroomBounce : MonoBehaviour
{
    [SerializeField] private float bounceForce = 15f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if(player != null)
            {
                player.Bounce(bounceForce);
            }
        }
    }
}