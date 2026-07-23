using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarController : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private int maxStages = 4;
    public int currentStage = 1;

    public static event Action<int> OnTimeStageChanged;

    private void Start()
    {
        scrollbar.direction = Scrollbar.Direction.RightToLeft; // can also just set this in Inspector
        scrollbar.value = 0f;
        scrollbar.onValueChanged.AddListener(UpdateText);
        UpdateText(scrollbar.value); // Display initial value and set initial visuals
    }
    private void Update()
    {
        float speed = 0.75f;

        if (Input.GetKey(KeyCode.LeftArrow))
            scrollbar.value += speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow))
            scrollbar.value -= speed * Time.deltaTime;
    }
    private void UpdateText(float value)
    {
        int newStage = Mathf.FloorToInt(value * maxStages) + 1;

        if (newStage > maxStages)
        {
            newStage = maxStages;
        }

        valueText.text = "Current stage: " + newStage.ToString();

        if (newStage != currentStage)
        {
            currentStage = newStage;
            OnTimeStageChanged?.Invoke(currentStage);
        }
        else
        {
            currentStage = newStage;
        }
    }
}