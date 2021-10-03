using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class Bullet : IPoolable
{
    public GameObject bulletObject;
    public InGameState owner;
    public Vector3 spawnspoint;
    public Rigidbody2D bulletRigidbody;

    public int damage = 10;
    private float raycastDistance = 0.4f;
    private float destroyTimer = 1;

    private bool hasHit;

    public RaycastHit2D hit;

    void IPoolable.OnEnableObject()
    {
        
    }

    void IPoolable.OnDisableObject()
    {
        spawnspoint = Vector3.zero;
        GameObject.Destroy(bulletObject);
        bulletRigidbody = null;
    }

    public void SpawnBullet(Transform _spawnpoint)
    {
        bulletObject = GameObject.Instantiate(Resources.Load("2DBulletPrefab") as GameObject, _spawnpoint.position, Quaternion.Euler(new Vector3(0, 0, _spawnpoint.rotation.eulerAngles.z - 90)));
        bulletRigidbody = bulletObject.GetComponent<Rigidbody2D>();
    }
    
    public void OnFixedUpdate()
    {
        CheckCollision();

        if (hasHit)
        {
            destroyTimer -= Time.deltaTime;
        }

        if (bulletRigidbody != null)
        {
            if (bulletRigidbody.velocity == Vector2.zero)
            {
                owner.bulletPool.ReturnObjectToPool(this);
            }
        }
    }

    public void CheckCollision()
    {
        int layerMask = 1 << 8 ;
        layerMask = ~layerMask;
        if (bulletObject != null )
        {
            if (hit = Physics2D.CircleCast(bulletObject.transform.position, raycastDistance, bulletObject.transform.TransformDirection(Vector3.forward), layerMask))
            {
                if (hit.collider.CompareTag("Enemy") && !hasHit)
                {
                    if (hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        EventManager<GameObject, int>.Invoke(EventType.ENEMY_HIT, hit.collider.gameObject, damage);
                    }
                    EventManager<Bullet, RaycastHit2D>.Invoke(EventType.BULLET_HIT, this, hit);
                    BulletHit();
                }
            }
        }
    }

    public void BulletHit()
    {
        hasHit = true;
        DestroyDelayTimer();

        Rigidbody2D rb = bulletObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.velocity = new Vector3(0,0,0);

        bulletObject.GetComponent<SpriteRenderer>().enabled = false;

        owner.bulletPool.ReturnObjectToPool(this);
    }

    private void DestroyDelayTimer()
    {
        if (destroyTimer <= 0)
        {
            owner.bulletPool.ReturnObjectToPool(this);
        }
    }
}
