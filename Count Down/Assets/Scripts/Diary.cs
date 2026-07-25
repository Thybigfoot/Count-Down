using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Diary : MonoBehaviour
{
    // Current level - should be connected to some general game handler object that knows current level
    private int currentLevel = 1;

    private int currentPage = 0;
    public int maxPages = 0;
    [SerializeField] private GameObject pagePrefab;


    private List<DiaryPage> pages = new();

    private void Start()
    {
        AddPage();
        gameObject.SetActive(false);
    }

    private void Update() {
        CheckPages();
    }

    private void CheckPages()
    {
        if (currentPage == 0)
        {

            pages[currentPage].FlipButton(false, false);
        }
        else
        {
            pages[currentPage].FlipButton(false, true);
        }

        if (currentPage == maxPages-1)
        {
            pages[currentPage].FlipButton(true, false);
        }
        else
        {
            pages[currentPage].FlipButton(true, true);
        }
    }

    public void AddPage()
    {
        if (pages.Count != 0)
        {
            // Close current page if opening a new one
            pages[currentPage].gameObject.SetActive(false);
        }
        maxPages += 1;

        GameObject page = Instantiate(pagePrefab, transform);
        page.GetComponent<DiaryPage>().Initialise(maxPages);

        currentPage = maxPages - 1;
        pages.Add(page.GetComponent<DiaryPage>());
    }

    public void PickupEvidence(PickupType type, Sprite sprite)
    {
        // Pass on parameters to the current page
        pages[currentLevel/3].PickupEvidence(type, sprite);
    }
    
    public void OnOpenButtonPressed()
    {
        if (gameObject.activeSelf)
        {
             gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        
    }
    public void OnExitButtonPressed()
    {
        gameObject.SetActive(false);
    }
    public void OnPreviousButtonPressed()
    {
        pages[currentPage].gameObject.SetActive(false);
        currentPage -= 1;
        pages[currentPage].gameObject.SetActive(true);
    }
    public void OnNextButtonPressed()
    {
        pages[currentPage].gameObject.SetActive(false);
        currentPage += 1;
        pages[currentPage].gameObject.SetActive(true);
    }
}