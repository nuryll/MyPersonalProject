using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public AudioClip winSound;       
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {

            GameManager.Instance.WinGame();

            // Play win sound
            if (winSound != null)
                audioSource.PlayOneShot(winSound);
        }
    }
}

