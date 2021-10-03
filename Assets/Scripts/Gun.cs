using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    //stuff is Public because of Decorator access
    public GameObject bulletPrefab;
    public GameObject gunBarrel;
    private InGameState owner;

    public int damage;
    public int magSize = 6;

    public float fireRate = 0.4f;
    public float shootPower = 4f;
    public float reloadTime = 1f;

    public bool isReloading;
    public bool autoFire;
    public bool isExplosive;

    public Color bulletColor;
    public Vector3 bulletSize = new Vector3(0.3f, 0.3f, 0.3f);
    public List<GunModifier> gunModifiers;

    private bool waitingForNextShot = false;
    private float shootTimer;
    private int Ammo = 6;

    public Gun(InGameState _owner, GameObject _gunBarrel, List<GunModifier> _gunModifiers)
    {
        this.owner = _owner;
        gunBarrel = _gunBarrel;
        gunModifiers = _gunModifiers;

        Ammo = magSize;
        EventManager<int, int>.Invoke(EventType.AMMO_CHANGED, Ammo, magSize);

        for (int i = 0; i < gunModifiers.Count; i++)
        {
            gunModifiers[i].tempGun = this;
            gunModifiers[i].OnGunStart();
        }
    }

    public void OnUpdate()
    {
        shootTimer -= Time.deltaTime;

        if (autoFire)
        {
            if (InputManager.instance.GetButton(KeyBindingActions.Shoot) == 1)
            {
                Shoot();
            }
        }
        else
        {
            if (InputManager.instance.GetButtonDown(KeyBindingActions.Shoot) == 1)
            {
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public virtual void Reload()
    {
        Ammo = magSize;
        EventManager<int, int>.Invoke(EventType.AMMO_CHANGED, Ammo, magSize);
    }

    public virtual void Shoot()
    {
        if (shootTimer <= 0)
        {
            waitingForNextShot = true;
            if (Ammo > 0)
            {
                EventManager.Invoke(EventType.GUN_SHOOT);

                foreach (GunModifier b in gunModifiers)
                {
                    b.OnGunShoot();
                }

                Bullet bulletScript = owner.bulletPool.RequestObject();
                bulletScript.SpawnBullet(gunBarrel.transform);
                bulletScript.owner = owner;

                bulletScript.bulletObject.GetComponent<Rigidbody2D>().AddForce(bulletScript.bulletObject.transform.up * shootPower, ForceMode2D.Impulse);
                bulletScript.bulletObject.transform.localScale = bulletSize;
                bulletScript.bulletObject.GetComponent<Renderer>().material.SetColor("_Emissive", bulletColor);
                bulletScript.bulletObject.GetComponent<ParticleSystem>().startColor = bulletColor;

                Ammo--;
                EventManager<int,int>.Invoke(EventType.AMMO_CHANGED, Ammo,magSize);
            }
            else
            {
                Reload();
            }
            shootTimer = fireRate;
        }
    }
}