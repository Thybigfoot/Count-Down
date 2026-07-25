using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    private GameObject diary;

    private Exit exit;

    private void Awake()
    {
        diary = GameObject.FindGameObjectWithTag("Diary");
        exit = FindFirstObjectByType<Exit>();
    }

    public void PickupEvidence(PickupType type, Sprite sprite)
    {
        diary.GetComponent<Diary>().PickupEvidence(type, sprite);
    }
}
