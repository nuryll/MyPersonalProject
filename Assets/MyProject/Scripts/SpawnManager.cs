using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Fast Food Prefabs (3 types)")]
    public GameObject[] foodPrefabs;
    public int foodCount = 10;
    public float foodYOffset = 0.3f;       // specific height for food

    [Header("Star Powerups")]
    public GameObject starPrefab;
    public int starCount = 3;
    public float starYOffset = 0.6f;       // stars can float slightly

    [Header("Heart Powerups")]
    public GameObject heartPrefab;
    public int heartCount = 2;
    public float heartYOffset = 0.5f;

    [Header("Enemy Prefabs (8 types)")]
    public GameObject[] enemyPrefabs;
    public int enemyCount = 6;
    public float enemyYOffset = 0f;        // usually aligned with ground

    [Header("Spawn Area")]
    public Vector3 spawnAreaSize = new Vector3(20, 0, 20);

    [Header("Respawn Settings (for items only)")]
    public bool respawnOverTime = false;
    public float respawnDelay = 10f;
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

    // ==========================
    // INITIAL SPAWN
    // ==========================
    void SpawnAll()
    {
        for (int i = 0; i < foodCount; i++) SpawnRandomFood();
        for (int i = 0; i < starCount; i++) SpawnStar();
        for (int i = 0; i < heartCount; i++) SpawnHeart();
        for (int i = 0; i < enemyCount; i++) SpawnRandomEnemy();
    }

    // ==========================
    // SPAWN FUNCTIONS
    // ==========================
    void SpawnRandomFood()
    {
        if (foodPrefabs == null || foodPrefabs.Length == 0)
        {
            Debug.LogWarning("No food prefabs assigned!");
            return;
        }
        int index = Random.Range(0, foodPrefabs.Length);
        Vector3 pos = GetRandomPosition(foodYOffset);
        Instantiate(foodPrefabs[index], pos, Quaternion.identity);
    }

    void SpawnStar()
    {
        if (starPrefab == null) return;
        Vector3 pos = GetRandomPosition(starYOffset);
        Instantiate(starPrefab, pos, Quaternion.identity);
    }

    void SpawnHeart()
    {
        if (heartPrefab == null) return;
        Vector3 pos = GetRandomPosition(heartYOffset);
        Instantiate(heartPrefab, pos, Quaternion.identity);
    }

    void SpawnRandomEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned!");
            return;
        }
        int index = Random.Range(0, enemyPrefabs.Length);
        Vector3 pos = GetRandomPosition(enemyYOffset);
        Instantiate(enemyPrefabs[index], pos, Quaternion.identity);
    }

    // Used for respawning random items (not enemies)
    void SpawnRandomItem()
    {
        int type = Random.Range(0, 3); // 0=food, 1=star, 2=heart
        switch (type)
        {
            case 0: SpawnRandomFood(); break;
            case 1: SpawnStar(); break;
            case 2: SpawnHeart(); break;
        }
    }

    // ==========================
    // POSITION + GIZMOS
    // ==========================
    Vector3 GetRandomPosition(float yOffset)
    {
        Vector3 center = transform.position;
        float x = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float z = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);
        return new Vector3(center.x + x, center.y + yOffset, center.z + z);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.9f, 0.7f, 0.1f, 0.3f); //Color.yellow with transparency. Makes easier to see and change area
        Gizmos.DrawCube(transform.position + Vector3.up * 0.5f,
                        new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.z));
    }
}
