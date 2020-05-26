using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform flyingEnemySpawnTransform;
    [SerializeField] private Transform enemySpawnTransform;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private FlyingEnemy flyingEnemyPrefab;
    [SerializeField] private Transform houseTransform;

    private float _timeToSpawn;

    private void Update()
    {
        HandleSpawning();
    }

    private void HandleSpawning()
    {
        if (_timeToSpawn > Time.time)
        {
            return;
        }

        _timeToSpawn = Time.time + spawnInterval;

        int randomNumber = Random.Range(0, 2);

        switch (randomNumber)
        {
            case 0:
                flyingEnemyPrefab.AssignTarget(houseTransform);
                Instantiate(flyingEnemyPrefab, flyingEnemySpawnTransform);
                break;
            case 1:
                Instantiate(enemyPrefab, enemySpawnTransform);
                break;
        }
    }
}
