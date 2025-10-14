using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    public int points = 10;
    public AudioClip pickupSound;  // Sound played when food is collected
    public float soundVolume = 0.8f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add points to score
            GameManager.Instance.AddScore(points);

            // Play sound effect at pickup position
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, soundVolume);
            }

            // Make player slower and larger
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GainWeight();
            }

            // Optional: particle or visual feedback
            Debug.Log("Collected fast food!");

            // Destroy food object
            Destroy(gameObject);
        }
    }

}
