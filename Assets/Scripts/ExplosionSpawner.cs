using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// is monobehaviour because explosion is purely cosmetic for now
public class ExplosionSpawner : MonoBehaviour
{
    public GameObject explosionFx;
    private bool collided;
    public Color explosionColor;

    private void Start()
    {
       // SpawnExplosion();
    }


    public void SpawnExplosion()
    {
        if (!collided)
        {
            GameObject explosion = Instantiate(explosionFx, transform.position, transform.rotation);
            explosion.GetComponent<ParticleSystem>().startColor = explosionColor;
           // explosion.GetComponent<ParticleSystem>().
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;

            collided = true;
            StartCoroutine(DeleteTimer());
        }

    }


    
    public IEnumerator DeleteTimer()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);

    }


}
