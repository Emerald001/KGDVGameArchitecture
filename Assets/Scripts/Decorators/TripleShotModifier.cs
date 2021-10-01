using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TripleShotModifier", order = 1)]
public class TripleShotModifier : GunModifier
{
    public int extraBulletAmount;
    public float spread;
    private float spreadInterval;


    public override void OnGunShoot()
    {
        spreadInterval = (spread * 2) / extraBulletAmount;

        for (int i = 0; i < extraBulletAmount; i++)
        {
            GameObject bullet = GameObject.Instantiate(tempGun.bulletPrefab = Resources.Load("BulletPrefab") as GameObject, tempGun.gunBarrel.transform.position, Quaternion.Euler(new Vector3(0,  tempGun.gunBarrel.transform.rotation.eulerAngles.y - spread + (spreadInterval * i), 0)));
            //add bullet damage to bullet here?
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * tempGun.shootPower, ForceMode.Impulse);
            bullet.transform.localScale = tempGun.bulletSize;
            bullet.GetComponent<Renderer>().material.SetColor("_EmissionColor", tempGun.bulletColor);
            bullet.GetComponent<ParticleSystem>().startColor = tempGun.bulletColor;
            base.OnGunShoot();
        }

    }
}
