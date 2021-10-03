using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    public List<EnemyAI> enemies;
    private Transform playerTransform;
    // moet casper ff aanpassen/gebruiken
    private List<Vector3> enemySpawnPoints;
    public EnemyManager(Transform _playerTransform, List<Vector3> _spawnPoints)
    {
        playerTransform = _playerTransform;
        enemies = new List<EnemyAI>();
        enemySpawnPoints = _spawnPoints;

        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (var i = 0; i < enemySpawnPoints.Count; i++)
        {
            enemies.Add(new EnemyAI(playerTransform, this));
            enemies[i].enemyObject = Object.Instantiate(Resources.Load("EnemyPrefab") as GameObject, enemySpawnPoints[i] , Quaternion.identity );
            enemies[i].OnStart();
        }
    }

    public void OnUpdate()
    {
        foreach (var enemy in enemies)
        {
            enemy.OnUpdate();
        }
    }
}