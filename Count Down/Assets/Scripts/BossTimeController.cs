using System.Collections;
using UnityEngine;

public class BossTimeController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The scene's ScrollbarController, so we can drive its stage.")]
    [SerializeField] private ScrollbarController scrollbarController;

    [Header("Timing")]
    [SerializeField] private float timeBetweenScrubs = 4f;
    [SerializeField] private float telegraphDuration = 1f;

    [Header("Pattern (stage numbers, 1-based to match their system)")]
    [SerializeField] private int[] pattern = { 1, 2, 3, 2 };

    private int patternIndex;

    private void Start()
    {
        StartCoroutine(ScrubLoop());
    }

    private IEnumerator ScrubLoop()
    {
        while (true)
        {
            int nextStage = pattern[patternIndex % pattern.Length];
            patternIndex++;

            yield return new WaitForSeconds(timeBetweenScrubs - telegraphDuration);

            // Telegraph — for now just a console message so we can see it working.
            Debug.Log("Boss telegraphing stage: " + nextStage);
            yield return new WaitForSeconds(telegraphDuration);

            // Drive the existing time system.
            scrollbarController.SetStage(nextStage);
        }
    }
}