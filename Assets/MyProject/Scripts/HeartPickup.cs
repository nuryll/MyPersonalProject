using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    [Header("Freeze Settings")]
    public float freezeDuration = 7f; // how long enemies stay frozen

    [Header("Sound Settings")]
    public AudioClip pickupSound;    
    public float soundVolume = 0.8f;

    public GameObject pickupParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Freeze all enemies
            Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            foreach (Enemy enemy in enemies)
            {
                enemy.FreezeAndSpin(freezeDuration);
            }

            // Play pickup sound at pickup position
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, soundVolume);
            }

            // Particle effect
            if (pickupParticle != null)
                Instantiate(pickupParticle, transform.position, Quaternion.identity);

            Debug.Log("Heart collected! Enemies frozen!");

            Destroy(gameObject);
        }
    }
}
