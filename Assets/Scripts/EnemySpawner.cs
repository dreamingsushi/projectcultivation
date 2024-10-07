using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;         // Prefab of the enemy to spawn
    public Transform[] spawnPoints;        // An array of spawn points (set these in the Unity Inspector)
    public float spawnInterval = 3f;       // Time between spawns in seconds
    public int maxEnemies = 5;             // Maximum number of enemies to spawn
    private int currentEnemyCount = 0;     // Track current enemy count

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Coroutine to repeatedly spawn enemies
    private IEnumerator SpawnEnemies()
    {
        while (currentEnemyCount < maxEnemies)
        {
            SpawnEnemyAtRandomPoint();      // Spawn the enemy
            currentEnemyCount++;            // Increase enemy count
            yield return new WaitForSeconds(spawnInterval);  // Wait for the next spawn
        }
    }

    // Method to spawn enemy at a random spawn point
    private void SpawnEnemyAtRandomPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);  // Pick a random spawn point
        Transform spawnPoint = spawnPoints[randomIndex];        // Get the selected spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);  // Spawn the enemy
    }
}
