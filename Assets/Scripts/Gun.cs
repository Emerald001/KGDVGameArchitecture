using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    //stuff is Public because of Decorator access
    public GameObject bulletPrefab;
    public int damage;
    public float fireRate = 0.4f;
    public float shootPower = 4f;
    public int magSize = 6;
    public float reloadTime = 1f;
    public bool isReloading;
    public GameObject gunBarrel;
    public bool autoFire;
    public Vector3 bulletSize = new Vector3(0.3f, 0.3f, 0.3f);
    public Color bulletColor;
    public List<GunModifier> gunModifiers;

    private ObjectPooler bulletPooler;
    private bool waitingForNextShot = false;
    private float shootTimer;
    private int Ammo = 6;

    public Gun(GameObject _gunBarrel, List<GunModifier> _gunModifiers, ObjectPooler _bulletPool)
    {
        gunBarrel = _gunBarrel;
        gunModifiers = _gunModifiers;

        Ammo = magSize;
        EventManager<int, int>.Invoke(EventType.AMMO_CHANGED, Ammo, magSize);

        for (int i = 0; i < gunModifiers.Count; i++)
        {
            //gunModifiers[i] = GameManager.Instance.gunModifiers[i];
            gunModifiers[i].tempGun = this;
            gunModifiers[i].OnGunStart();
        }
        //EventManager.Subscribe(EventType.GUN_SHOOT, Shoot);
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
        //do reloading stuff
       // yield return new WaitForSeconds(reloadTime);
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
                //shoot bullet
                GameObject bullet = GameObject.Instantiate(bulletPrefab = Resources.Load("2DBulletPrefab") as GameObject, gunBarrel.transform.position,Quaternion.Euler(new Vector3(0,0, gunBarrel.transform.rotation.eulerAngles.z - 90) ));
                //GameObject bullet = bulletPooler.SpawnFromPool("Bullet", gunBarrel.transform.position, gunBarrel.transform.rotation);

                //add bullet damage to bullet here?
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * shootPower, ForceMode2D.Impulse);
                bullet.transform.localScale = bulletSize;
                bullet.GetComponent<Renderer>().material.SetColor("_Emissive", bulletColor);
                bullet.GetComponent<ParticleSystem>().startColor = bulletColor;
                Ammo--;
                //later een keer UI manager maken ofzo?
                EventManager<int,int>.Invoke(EventType.AMMO_CHANGED, Ammo,magSize);
            }
            shootTimer = fireRate;
        }
    }
}
