using UnityEngine;

public class DinoTimeLock : MonoBehaviour
{
    [SerializeField] private BossTimeController boss;
    [SerializeField] private Transform pointB;
    [SerializeField] private float arriveThreshold = 0.2f;

    private bool playerOnBoard;
    private bool locked;

    void OnDisable()
    {
        Release(); // safety: always release if the dino turns off
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            playerOnBoard = true;
            Lock();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            playerOnBoard = false;
            Release();
        }
    }

    void Update()
    {
        // Release once the dino reaches pointB (ride complete).
        if (locked && pointB != null &&
            Vector3.Distance(transform.position, pointB.position) <= arriveThreshold)
        {
            Release();
        }
    }

    private void Lock()
    {
        if (boss != null && !locked)
        {
            boss.SetPaused(true);
            locked = true;
        }
    }

    private void Release()
    {
        if (boss != null && locked)
        {
            boss.SetPaused(false);
            locked = false;
        }
    }
}