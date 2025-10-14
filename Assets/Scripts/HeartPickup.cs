using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public float freezeDuration = 7f; // how long enemies stay frozen

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None); // Find all enemies in the scene

            foreach (Enemy enemy in enemies)
            {
                enemy.FreezeAndSpin(freezeDuration);
            }

            // Optional: add particle/sound effects here
            Destroy(gameObject);
        }
    }
}
