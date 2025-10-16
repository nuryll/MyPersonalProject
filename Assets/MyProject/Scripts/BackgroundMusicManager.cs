using UnityEngine;


public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance;

    [Header("Music Settings")]
    public AudioClip backgroundMusic;
    [Range(0f, 1f)] public float volume = 0.5f;

    private AudioSource audioSource;

    void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Setup AudioSource only once
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    void Start()
    {
        // Play music only if not already playing
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void PlayMusic()
    {
        if (audioSource != null && backgroundMusic != null && !audioSource.isPlaying)
            audioSource.Play();
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }

    public void SetVolume(float newVolume)
    {
        if (audioSource != null)
            audioSource.volume = Mathf.Clamp01(newVolume);
    }
}

