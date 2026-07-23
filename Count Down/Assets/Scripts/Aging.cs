using UnityEngine;

public class Aging : MonoBehaviour
{
    [SerializeField] private GameObject[] stageObjects; // assign in Inspector, in stage order

    private void OnEnable()
    {
        ScrollbarController.OnTimeStageChanged += UpdateStageVisuals;
    }

    private void OnDisable()
    {
        ScrollbarController.OnTimeStageChanged -= UpdateStageVisuals;
    }

    private void Start()
    {
        UpdateStageVisuals(1); // default to stage 3 (present) at scene start
    }

    private void UpdateStageVisuals(int stage)
    {
        for (int i = 0; i < stageObjects.Length; i++)
        {
            stageObjects[i].SetActive(i == stage - 1);
        }
    }
}