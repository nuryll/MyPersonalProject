using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    public int points = 10;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add points to score
            GameManager.Instance.AddScore(points);

            // Optional: play particle or sound later
            Debug.Log("Collected fast food!");

            // Destroy object
            Destroy(gameObject);

            // Make player slower and larger
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GainWeight();
            }

            Destroy(gameObject);
        }
    }
    
}
