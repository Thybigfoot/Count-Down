using UnityEngine;

public class StageVisuals : MonoBehaviour
{
    [SerializeField] private GameObject[] stageObjects; // assign in Inspector, in stage order

    private void OnEnable()
    {
        temp_timelogic.OnTimeStageChanged += UpdateStageVisuals;
    }

    private void OnDisable()
    {
        temp_timelogic.OnTimeStageChanged -= UpdateStageVisuals;
    }

    private void Start()
    {
        UpdateStageVisuals(0); // set correct initial state
    }

    private void UpdateStageVisuals(int stage)
    {
        for (int i = 0; i < stageObjects.Length; i++)
        {
            stageObjects[i].SetActive(i == stage);
        }
    }
}