using UnityEngine;
using Interfaces;

public class Bullet : IPoolable
{
    public GameObject bulletObject;
    public InGameState owner;
    private Vector3 spawnsPoint;
    private Rigidbody2D bulletRigidbody;

    private int damage = 10;
    private float raycastDistance = 0.4f;
    private float destroyTimer = 1;

    private bool hasHit;

    private RaycastHit2D hit;

    void IPoolable.OnEnableObject()
    {
        
    }

    void IPoolable.OnDisableObject()
    {
        spawnsPoint = Vector3.zero;
        Object.Destroy(bulletObject);
        bulletRigidbody = null;
    }

    public void SpawnBullet(Transform _spawnPoint)
    {
        bulletObject = Object.Instantiate(Resources.Load("2DBulletPrefab") as GameObject, _spawnPoint.position, Quaternion.Euler(new Vector3(0, 0, _spawnPoint.rotation.eulerAngles.z - 90)));
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

    private void CheckCollision()
    {
        var layerMask = 1 << 8 ;
        layerMask = ~layerMask;
        if (bulletObject != null )
        {
            if (hit == Physics2D.CircleCast(bulletObject.transform.position, raycastDistance, bulletObject.transform.TransformDirection(Vector3.forward), layerMask))
            {
                if (hit.collider.CompareTag("Enemy") && !hasHit)
                {
                    if (hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        EventManager<GameObject, int>.Invoke(EventType.enemyHit, hit.collider.gameObject, damage);
                    }
                    EventManager<Bullet, RaycastHit2D>.Invoke(EventType.bulletHit, this, hit);
                    BulletHit();
                }
            }
        }
    }

    private void BulletHit()
    {
        hasHit = true;
        DestroyDelayTimer();

        var rb = bulletObject.GetComponent<Rigidbody2D>();
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
