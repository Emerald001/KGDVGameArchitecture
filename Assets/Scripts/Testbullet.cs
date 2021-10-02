using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testbullet : MonoBehaviour
{
    public ExplosionSpawner explosionSpawner;
    public Color FXcolor;

    private void Start()
    {
        explosionSpawner = GetComponent<ExplosionSpawner>();    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        {           
            explosionSpawner.explosionColor = FXcolor;
            explosionSpawner.SpawnExplosion();
        }
    }
}
