using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private Diary Diary;

    private Exit exit;

    private void Awake()
    {
        exit = FindFirstObjectByType<Exit>();
    }

    public void PickupEvidence(PickupType type, Sprite sprite)
    {
        Diary.PickupEvidence(type, sprite);
    }
}
