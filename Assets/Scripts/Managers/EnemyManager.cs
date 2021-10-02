using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    public List<EnemyAI> enemies;
    public int enemyAmount = 6;
    //tijdelijk? moet casper ff aanpassen/gebruiken
    public Transform[] enemySpawnPoints;
    public EnemyManager()
    {
        enemies = new List<EnemyAI>();
        SpawnEnemies(enemyAmount);
        //spawn enemies in grid and add to list here?
    }

    public void SpawnEnemies(int _spawnAmount)
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            enemies.Add(new EnemyAI());
            enemies[i].enemyObject = Object.Instantiate(Resources.Load("2DBulletPrefab") as GameObject, enemySpawnPoints[i].position, Quaternion.identity );
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
