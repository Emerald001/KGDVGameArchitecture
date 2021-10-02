using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI
{
    public Transform player;

    public GameObject enemyObject;
    public float moveSpeedEnemy = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public EnemyAI()
    {
        rb = enemyObject.GetComponent<Rigidbody2D>();

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


    void MoveCharacter(Vector2 _direction)
    {
        rb.MovePosition((Vector2)enemyObject.transform.position + (_direction * moveSpeedEnemy * Time.deltaTime));
    }
}