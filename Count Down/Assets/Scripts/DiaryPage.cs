using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryPage : MonoBehaviour
{
    private Dictionary<string, Image> evidence = new Dictionary<string, Image>();
    private int found = 0;

    [Header("Page flipping")]
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject previousButton;

    public int PageNumber { get; private set; }

    public void Initialise(int pageNumber)
    {
        PageNumber = pageNumber;
    }

    private void Awake()
    {
        // Set evidence
        foreach (Image image in GetComponentsInChildren<Image>(true))
        {
            evidence[image.gameObject.name] = image;
        }
    }

    public void PickupEvidence(PickupType type, Sprite sprite)
    {
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
