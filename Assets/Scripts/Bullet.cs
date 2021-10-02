using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : IPoolable
{
    bool IPoolable.Active { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public GameObject bulletObject;
    public int damage;
    BulletManager bulletManager;
    private float raycastDistance = 2f;
    public Color bulletColor;

    public bool explosive;
    private bool hasHit;

    public RaycastHit2D hit;

    public Bullet(BulletManager _bulletManager)
    {
        bulletManager = _bulletManager;
    }

    void IPoolable.OnDisableObject()
    {
        throw new System.NotImplementedException();
    }

    void IPoolable.OnEnableObject()
    {
        throw new System.NotImplementedException();
    }
    
    public void OnUpdate()
    {
        CheckCollision();
    }

    public void CheckCollision()
    {
        int layerMask = 1 << 8 ;
        layerMask = ~layerMask;
        //Debug.Log("bulletCollision checked");

        if (hit = Physics2D.CircleCast(bulletObject.transform.position, raycastDistance, bulletObject.transform.TransformDirection(Vector3.forward), layerMask))
        {
            if (hit.collider.CompareTag("Walls") || hit.collider.CompareTag("Enemy") && !hasHit)
            {
                EventManager<Bullet, RaycastHit2D>.Invoke(EventType.BULLET_HIT, this, hit);
                Debug.Log("bullet hit: " + hit.collider.gameObject.name);
                //BulletHit();
            }
        }
    }

    public void BulletHit()
    {
        hasHit = true;

        Object.Destroy(bulletObject);
        bulletManager.bullets.Remove(this);
        if (explosive)
        {
            //spawn explosion

        }
    }
}
