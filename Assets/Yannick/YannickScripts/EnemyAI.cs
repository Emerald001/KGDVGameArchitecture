using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Interfaces.IDamagable
{
    public Transform player;
    public EnemyManager enemyManager;
    int enemyHealth = 20;
    public GameObject enemyObject;
    public float moveSpeedEnemy = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public EnemyAI(Transform _playerTransform, EnemyManager _enemyManager)
    {
        enemyManager = _enemyManager;
        player = _playerTransform;
    }

    public void OnStart()
    {
        rb = enemyObject.GetComponent<Rigidbody2D>();
        EventManager<GameObject, int>.Subscribe(EventType.ENEMY_HIT, CheckDamager);
    }


    // Start is called before the first frame update

    // Update is called once per frame
    public void OnUpdate()
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
            enemyManager.enemies.Remove(this);
            Object.Destroy(enemyObject);
        }
    }


    void MoveCharacter(Vector2 _direction)
    {
        rb.MovePosition((Vector2)enemyObject.transform.position + (_direction * moveSpeedEnemy * Time.deltaTime));
    }
}