using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeedEnemy = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        float distance = Vector3.Distance(player.position, transform.position);

        if(distance > 2)
        {
            MoveCharacter(movement);
        }
    }

   
    void MoveCharacter(Vector2 _direction)
    {
        rb.MovePosition((Vector2)transform.position + (_direction * moveSpeedEnemy * Time.deltaTime));
    }
}
