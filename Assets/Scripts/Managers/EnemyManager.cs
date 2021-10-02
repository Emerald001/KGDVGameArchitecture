using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    public List<EnemyAI> enemies;
    public Transform playerTransform;
    public int enemyAmount = 20;
    // moet casper ff aanpassen/gebruiken
    public Vector3[] enemySpawnPoints;
    public EnemyManager(Transform _playerTransform)
    {
        playerTransform = _playerTransform;
        enemies = new List<EnemyAI>();
        enemySpawnPoints = new Vector3[1];

        enemySpawnPoints[0] = new Vector3(0, 0, 0);

        SpawnEnemies(enemyAmount);
    }

    public void SpawnEnemies(int _spawnAmount)
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            enemies.Add(new EnemyAI(playerTransform, this));                                     //hier een randomizer plaatsen ofzo V
            enemies[i].enemyObject = Object.Instantiate(Resources.Load("EnemyPrefab") as GameObject,enemySpawnPoints[0] , Quaternion.identity );
            enemies[i].OnStart();
        }
    }

    public void OnUpdate()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].OnUpdate();
        }
    }
}
