using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarController : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private int maxStages = 3;
    public int currentStage = 1;

    private void Start()
    {
        scrollbar.onValueChanged.AddListener(UpdateText);
        UpdateText(scrollbar.value); // Display initial value
    }

    private void UpdateText(float value)
    {
        currentStage = Mathf.CeilToInt(maxStages * value);

        if (currentStage == 0)
        {
            currentStage = 1;
        }
        valueText.text = "Current stage: " + currentStage.ToString();
    }
}
