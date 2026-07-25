using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{
    [SerializeField] private Sprite openedSprite;
    private bool opened = false;
    private int index;
    private int nextIndex;
    private void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        nextIndex = index + 1;
    }

    public void OpenDoor()
    {
        opened = true;
        GetComponent<SpriteRenderer>().sprite = openedSprite;
    }
    private void Update() {
        if (FindObjectsByType<Evidence>(FindObjectsSortMode.None).Length == 0)
        {
            // Allow exit
            OpenDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (!other.CompareTag("Player"))
            return;

        // Player should exit here
        if (other.CompareTag("Player") && opened == true)
        {
            if(nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextIndex);
            }
            else
            {
                Debug.LogError($"Scene does not exist{nextIndex}");
            }
        }
    }
}
