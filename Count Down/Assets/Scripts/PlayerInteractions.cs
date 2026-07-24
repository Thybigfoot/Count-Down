using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private Image MurdererUI;
    [SerializeField] private Image WeaponUI;
    [SerializeField] private Image VictimUI;
    private bool murdererFound = false;
    private bool weaponFound = false;
    private bool victimFound = false;

    private Exit exit;

    private void Awake()
    {
        exit = FindFirstObjectByType<Exit>();
    }

    public void PickupEvidence(PickupType type, Sprite sprite)
    {
        switch (type)
        {
            case PickupType.Murderer:
                MurdererUI.sprite = sprite;
                murdererFound = true;
                break;

            case PickupType.Weapon:
                WeaponUI.sprite = sprite;
                weaponFound = true;
                break;

            case PickupType.Victim:
                VictimUI.sprite = sprite;
                victimFound = true;
                break;
        }

        if (murdererFound && weaponFound && victimFound)
        {
            exit.OpenDoor();
        }
    }
}
