using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    public GameObject bulletPrefab;

    public int damage;
    public float fireRate = 0.4f;

    public float shootPower = 6;

    public int magSize = 6;
    private int Ammo = 6;
    public float reloadTime = 1f;
    public bool isReloading;
    public GameObject gunBarrel;
    public bool autoFire;
    private bool waitingForNextShot = false;
    private float shootTimer;

    public Vector3 bulletSize = new Vector3 (0.3f, 0.3f, 0.3f);
    public Color bulletColor;
    public List<GunModifier> gunModifiers;

    public void GunStart(List<GunModifier> _gunModifiers)
    {
        gunModifiers = _gunModifiers;
        gunBarrel = GameManager.Instance.gunObject;

        Ammo = magSize;
        for (int i = 0; i < GameManager.Instance.gunModifiers.Count; i++)
        {
            //mag dit? ik weet zo geen andere workaround

            gunModifiers[i] = GameManager.Instance.gunModifiers[i];
            gunModifiers[i].tempGun = this;
            gunModifiers[i].OnGunStart();
            Debug.Log("start can gun werk blabal");

        }
        //EventManager.Subscribe(EventType.GUN_SHOOT, Shoot);
    }

    public void GunUpdate()
    {
        shootTimer -= Time.deltaTime;

        if (autoFire)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

    }

    public virtual IEnumerator Reload()
    {
        //do reloading stuff
        yield return new WaitForSeconds(reloadTime);
        Ammo = magSize;
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
                GameObject bullet = GameObject.Instantiate(bulletPrefab = Resources.Load("BulletPrefab") as GameObject, gunBarrel.transform.position, gunBarrel.transform.rotation);
                //add bullet damage to bullet here?
                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * shootPower, ForceMode.Impulse);
                bullet.transform.localScale = bulletSize;
                bullet.GetComponent<Renderer>().material.SetColor("_Emissive", bulletColor);
                bullet.GetComponent<ParticleSystem>().startColor = bulletColor;
                Debug.Log("gun shot");
                Ammo--;
                //later een keer UI manager maken ofzo?
                EventManager.Invoke(EventType.AMMO_CHANGED);

            }

            shootTimer = fireRate;
        }

    

    }
}
