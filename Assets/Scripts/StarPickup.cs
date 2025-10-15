using UnityEngine;

public class StarPickup : MonoBehaviour
{
    [Header("Sound Settings")]
    public AudioClip pickupSound;      
    public float soundVolume = 0.8f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Give player the speed boost
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GainSpeed();
            }

            // Play pickup sound at pickup position
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, soundVolume);
            }

            Debug.Log("Star collected! Speed increased!");

            Destroy(gameObject);
        }
    }
}
