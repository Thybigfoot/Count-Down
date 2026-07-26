using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private AudioClip pageTurnSound;
    [SerializeField] private AudioClip evidenceCollection;
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    public void PlayPageTurn()
    {
        audioSource.PlayOneShot(pageTurnSound);
    }

    public void PlayEvidenceCollection()
    {
        audioSource.PlayOneShot(evidenceCollection);
    }
}
