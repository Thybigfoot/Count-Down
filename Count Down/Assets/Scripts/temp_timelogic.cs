using UnityEngine;
using System;
public class temp_timelogic : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddTime();
        }
    }
    private int TimeStage = 0;
    public static event Action<int> OnTimeStageChanged;
    public void AddTime()
    {
        TimeStage++;
        OnTimeStageChanged?.Invoke(TimeStage);
    }
}
