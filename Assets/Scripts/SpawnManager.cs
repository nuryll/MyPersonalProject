using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Fast Food Prefabs (3 types)")]
    public GameObject[] foodPrefabs;      // Assign 3 fast food prefabs here
    public int foodCount = 10;            // Number of fast food to spawn

    [Header("Star Powerups")]
    public GameObject starPrefab;
    public int starCount = 3;             // Few stars only

    [Header("Heart Powerups")]
    public GameObject heartPrefab;
    public int heartCount = 2;            // Even fewer hearts

    [Header("Spawn Area")]
    public Vector3 spawnAreaSize = new Vector3(20, 0, 20);
    public float yOffset = 0.5f;          // Height above the floor

    [Header("Respawn Settings")]
    public bool respawnOverTime = false;
    public float respawnDelay = 10f;      // Seconds between respawn
    private float nextSpawnTime;

    void Start()
    {
        SpawnAll();
    }

    void Update()
    {
        if (respawnOverTime && Time.time >= nextSpawnTime)
        {
            SpawnRandomItem();
            nextSpawnTime = Time.time + respawnDelay;
        }
    }

    // Spawn all items at start
    void SpawnAll()
    {
        for (int i = 0; i < foodCount; i++)
            SpawnRandomFood();

        for (int i = 0; i < starCount; i++)
            SpawnStar();

        for (int i = 0; i < heartCount; i++)
            SpawnHeart();
    }

    // ==========================
    // SPAWN METHODS
    // ==========================

    void SpawnRandomFood()
    {
        if (foodPrefabs == null || foodPrefabs.Length == 0)
        {
            Debug.LogWarning("No food prefabs assigned in FoodAndPowerupSpawnManager!");
            return;
        }

        int index = Random.Range(0, foodPrefabs.Length);
        Vector3 pos = GetRandomPosition();
        Instantiate(foodPrefabs[index], pos, Quaternion.identity);
    }

    void SpawnStar()
    {
        if (starPrefab == null) return;
        Vector3 pos = GetRandomPosition();
        Instantiate(starPrefab, pos, Quaternion.identity);
    }

    void SpawnHeart()
    {
        if (heartPrefab == null) return;
        Vector3 pos = GetRandomPosition();
        Instantiate(heartPrefab, pos, Quaternion.identity);
    }

    // Randomly spawn one of any item (used for respawn)
    void SpawnRandomItem()
    {
        int type = Random.Range(0, 3); // 0 = food, 1 = star, 2 = heart

        switch (type)
        {
            case 0: SpawnRandomFood(); break;
            case 1: SpawnStar(); break;
            case 2: SpawnHeart(); break;
        }
    }

    // Get a random position inside the spawn area
    Vector3 GetRandomPosition()
    {
        Vector3 center = transform.position;
        float x = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float z = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);

        return new Vector3(center.x + x, center.y + yOffset, center.z + z);
    }

    // Visualize the spawn area in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.7f, 0f, 0.3f);
        Gizmos.DrawCube(transform.position + Vector3.up * yOffset,
                        new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.z));
    }
}
