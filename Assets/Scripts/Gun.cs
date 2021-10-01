using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    //stuff is Public because of Decorator access
    public GameObject bulletPrefab;
    public int damage;
    public float fireRate = 0.4f;
    public float shootPower = 3f;
    public int magSize = 10;
    public float reloadTime = 1f;
    public bool isReloading;
    public GameObject gunBarrel;
    public bool autoFire;
    public Vector3 bulletSize = new Vector3(0.3f, 0.3f, 0.3f);
    public Color bulletColor;
    public List<GunModifier> gunModifiers;

    private bool waitingForNextShot = false;
    private float shootTimer;
    private int Ammo = 6;

    public Gun(GameObject _gunBarrel, List<GunModifier> _gunModifiers)
    {
        gunBarrel = _gunBarrel;
        gunModifiers = _gunModifiers;
        Debug.Log(gunModifiers.Count);
    }

    public void OnEnter()
    {
       // currentGun = Object.Instantiate(gunBarrel, )
        Ammo = magSize;
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            //StartCoroutine(Reload());
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
                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.up * shootPower, ForceMode.Impulse);
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
