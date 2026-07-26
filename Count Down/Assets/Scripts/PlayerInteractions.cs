using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{

    private Exit exit;

    private void Awake()
    {
        exit = FindFirstObjectByType<Exit>();
    }

    public void PickupEvidence(PickupType type, Sprite sprite)
    {
        Diary.Instance.GetComponent<Diary>().PickupEvidence(type, sprite);
    }
}
