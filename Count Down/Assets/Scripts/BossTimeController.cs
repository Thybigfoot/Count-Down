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
    [SerializeField] private int[] pattern = { 1, 2, 3, 4, 3, 2 };

    private int patternIndex;
    private bool paused;

    public void SetPaused(bool value)
    {
        paused = value;
    }

    private void Start()
    {
        StartCoroutine(ScrubLoop());
    }

    private IEnumerator ScrubLoop()
    {
        while (true)
        {
            // If paused (e.g. player is riding the dino), wait here without scrubbing.
            while (paused)
                yield return null;

            int nextStage = pattern[patternIndex % pattern.Length];
            patternIndex++;

            yield return new WaitForSeconds(timeBetweenScrubs - telegraphDuration);

            // Don't scrub if we got paused during the wait.
            while (paused)
                yield return null;

            Debug.Log("Boss telegraphing stage: " + nextStage);
            yield return new WaitForSeconds(telegraphDuration);

            while (paused)
                yield return null;

            scrollbarController.SetStage(nextStage);
        }
    }
}