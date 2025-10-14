using UnityEngine;

public class StarPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GainSpeed();
            }

            // Optional: sparkle particle or sound
            Destroy(gameObject);
        }
    }
}
