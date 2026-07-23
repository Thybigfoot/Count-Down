using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private Sprite openedSprite;
    private bool opened = false;

    public void OpenDoor()
    {
        opened = true;
        GetComponent<SpriteRenderer>().sprite = openedSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (!other.CompareTag("Player"))
            return;

        // Player should exit here
    }
}
