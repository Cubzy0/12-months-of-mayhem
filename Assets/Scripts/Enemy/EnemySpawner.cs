using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public EnemyController enemyPrefab;  // your Enemy_Snowman prefab
    public float spawnInterval = 2f;
    public int maxEnemies = 30;

    [Header("Spawn Area")]
    public float spawnRadius = 8f;   // distance from center to spawn
    public float minDistanceFromPlayer = 2f;

    private float timer;
    private Transform player;

    private void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    private void Update()
    {
        if (enemyPrefab == null || player == null) return;

        // Don’t spawn if we already have many enemies
        int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (currentEnemies >= maxEnemies) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos;

        // keep trying until we find a spot far enough from player
        int safety = 0;
        do
        {
            safety++;
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            spawnPos = (Vector2)player.position + randomDir * spawnRadius;
        } 
        while (Vector2.Distance(spawnPos, player.position) < minDistanceFromPlayer && safety < 20);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}