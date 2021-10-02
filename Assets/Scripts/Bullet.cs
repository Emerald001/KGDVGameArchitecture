using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : IPoolable
{
    bool IPoolable.Active { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public GameObject bulletObject;
    public int damage = 10;
    BulletManager bulletManager;
    private float raycastDistance = 0.4f;
    public Color bulletColor;

    public bool explosive;
    private bool hasHit;

    private float destroyTimer = 1;

    public RaycastHit2D hit;

    public Bullet(BulletManager _bulletManager, Color _bulletColor, bool _explosive)
    {
        bulletManager = _bulletManager;
        bulletColor = _bulletColor;
        explosive = _explosive;
    }

    void IPoolable.OnDisableObject()
    {
        throw new System.NotImplementedException();
    }

    void IPoolable.OnEnableObject()
    {
        throw new System.NotImplementedException();
    }
    
    public void OnFixedUpdate()
    {
        CheckCollision();

        if (hasHit)
        {
            destroyTimer -= Time.deltaTime;
        }
    }

    public void CheckCollision()
    {
        int layerMask = 1 << 8 ;
        layerMask = ~layerMask;
        //Debug.Log("bulletCollision checked");
        if (bulletObject != null )
        {
            if (hit = Physics2D.CircleCast(bulletObject.transform.position, raycastDistance, bulletObject.transform.TransformDirection(Vector3.forward), layerMask))
            {
                if (hit.collider.CompareTag("Enemy") && !hasHit)
                {
                    //de Idamageable getten zou beter zijn, maar kan niet zonder components
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
        bulletManager.bullets.Remove(this);

        if (explosive)
        {
            GameObject explosion = Object.Instantiate(Resources.Load("EplosionSpawnerPrefab") as GameObject, bulletObject.transform.position, bulletObject.transform.rotation);
            //ja ik weet het sorry explosionspawner is een monobehaviour maar het is puur cosmetisch!
            ExplosionSpawner expSpawner = explosion.GetComponent<ExplosionSpawner>();
            expSpawner.explosionColor = bulletColor;
            expSpawner.SpawnExplosion();
        }
    }

    private void DestroyDelayTimer()
    {
        if (destroyTimer <= 0)
        {
            Object.Destroy(bulletObject);

        }
    }
}
