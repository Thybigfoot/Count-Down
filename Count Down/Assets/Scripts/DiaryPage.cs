using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiaryPage : MonoBehaviour
{
    private Dictionary<string, Image> evidence = new Dictionary<string, Image>();
    private int found = 0;

    [Header("Page flipping")]
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject previousButton;
    [SerializeField] private TextMeshProUGUI number;

    public int pageNumber { get; private set; }

    public void Initialise(int pN)
    {
        pageNumber = pN;
        number.text = pageNumber.ToString();

        // Set evidence
        foreach (Image image in GetComponentsInChildren<Image>(true))
        {
            evidence[image.gameObject.name] = image;
        }
    }
    
    public void PickupEvidence(PickupType type, Sprite sprite)
    {
        Debug.Log(evidence[type.ToString()]);
        evidence[type.ToString()].sprite = sprite;

        found += 1; 
    }

    public void FlipButton(bool nextBut, bool active)
    {
        if (nextBut)
        {
            nextButton.SetActive(active);
        }
        else
        {
            previousButton.SetActive(active);
        }
    }

    public void OnExitButtonPressed()
    {
        GetComponentInParent<Diary>().OnExitButtonPressed();
    }
    public void OnPreviousButtonPressed()
    {
        GetComponentInParent<Diary>().OnPreviousButtonPressed();
    }
    public void OnNextButtonPressed()
    {
        GetComponentInParent<Diary>().OnNextButtonPressed();
    }
}
