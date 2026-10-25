
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnDistance = 15f;
    [SerializeField] private int maxEnemies = 20;

    private float timer;

    private void Update()
    {
        if (GameManager.IsGameOver || !player) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            if (EnemyBase.ActiveEnemies.Count < maxEnemies)
                SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        Vector3 direction = new Vector3(
            Mathf.Cos(angle), 0f, Mathf.Sin(angle)
        );

        Vector3 spawnPosition = player.position + direction * spawnDistance;

        Enemy enemy = Instantiate(
            enemyPrefab, spawnPosition, Quaternion.identity
        );
        
        GameColor randomColor = (GameColor)Random.Range(0, 3);
        enemy.SetColor(randomColor);
    }
}
