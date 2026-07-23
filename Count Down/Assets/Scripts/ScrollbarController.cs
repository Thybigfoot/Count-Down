using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarController : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private TextMeshProUGUI valueText;
    private int maxStages = 3;
    public int currentStage = 1;

    private void Start()
    {
        scrollbar.onValueChanged.AddListener(UpdateText);
        UpdateText(scrollbar.value); // Display initial value
    }

    private void UpdateText(float value)
    {
        currentStage = Mathf.CeilToInt(maxStages * value);
        valueText.text = "Current stage: " + currentStage.ToString();
    }
}
