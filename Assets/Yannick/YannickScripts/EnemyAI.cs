using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Interfaces.IPoolable
{
    public GameObject enemyObject;
    private InGameState owner;
    private Transform player;
    private Rigidbody2D rb;

    private int enemyHealth = 20;
    private float moveSpeedEnemy = 5f;
    private Vector2 movement;

    public void OnStart(Vector3 _spawnpoint, Transform _playerTransform, InGameState _owner)
    {
        enemyObject = Object.Instantiate(Resources.Load("EnemyPrefab") as GameObject, _spawnpoint, Quaternion.identity);
        player = _playerTransform;
        owner = _owner;

        rb = enemyObject.GetComponent<Rigidbody2D>();
        EventManager<GameObject, int>.Subscribe(EventType.ENEMY_HIT, CheckDamager);
    }

    public void OnEnableObject()
    {

    }

    public void OnDisableObject()
    {
        GameObject.Destroy(enemyObject);
        player = null;
    }

    public void OnFixedUpdate()
    {
        Vector2 direction = player.position - enemyObject.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        float distance = Vector3.Distance(player.position, enemyObject.transform.position);

        if (distance > 2)
        {
            MoveCharacter(movement);
        }
    }

    private void CheckDamager(GameObject _hitEnemy, int _damage)
    {
        if (_hitEnemy == enemyObject)
        {
            TakeDamage(_damage);
        }
    }

    public void TakeDamage(int _damage)
    {
        enemyHealth -= _damage;
        if (enemyHealth <= 0)
        {
            owner.enemypool.ReturnObjectToPool(this);
            Object.Destroy(enemyObject);
        }
    }

    void MoveCharacter(Vector2 _direction)
    {
        rb.MovePosition((Vector2)enemyObject.transform.position + (_direction * moveSpeedEnemy * Time.deltaTime));
    }
}