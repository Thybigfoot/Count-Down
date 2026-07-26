using UnityEngine;

public class Evidence : MonoBehaviour
{
    // Pick a type from Weapon, Murderer and Victim
    [SerializeField] private PickupType type;

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (!other.CompareTag("Player"))
            return;

        other.GetComponent<PlayerInteractions>().PickupEvidence(type, GetComponent<SpriteRenderer>().sprite);

        Destroy(gameObject);
    }
}
