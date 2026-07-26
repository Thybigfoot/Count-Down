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
    public bool allowPlayerInput = true;

    public static event Action<int> OnTimeStageChanged;

    private void Start()
    {
        if (scrollbar != null)
        {
            scrollbar.direction = Scrollbar.Direction.RightToLeft;
            scrollbar.value = 0f;
            scrollbar.onValueChanged.AddListener(UpdateText);
        }
        UpdateStage(1);
    }

    private void Update()
    {
        if (!allowPlayerInput || scrollbar == null) return;

        float speed = 0.75f;

        if (Input.GetKey(KeyCode.LeftArrow))
            scrollbar.value += speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow))
            scrollbar.value -= speed * Time.deltaTime;
    }

    private void UpdateText(float value)
    {
        int newStage = Mathf.FloorToInt(value * maxStages) + 1;
        if (newStage > maxStages) newStage = maxStages;
        UpdateStage(newStage);
    }

    public void SetStage(int stage)
    {
        UpdateStage(stage);
    }

    private void UpdateStage(int newStage)
    {
        newStage = Mathf.Clamp(newStage, 1, maxStages);

        if (valueText != null)
            valueText.text = "Current stage: " + newStage.ToString();

        if (newStage != currentStage)
        {
            currentStage = newStage;
            OnTimeStageChanged?.Invoke(currentStage);
        }
    }
}