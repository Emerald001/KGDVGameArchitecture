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
            GameObject bullet = GameObject.Instantiate(Resources.Load("2DBulletPrefab") as GameObject, tempGun.gunBarrel.transform.position, Quaternion.Euler(new Vector3(0 ,0 ,(tempGun.gunBarrel.transform.rotation.eulerAngles.z - spread + (spreadInterval * i)) -90 )));
            //add bullet damage to bullet here?
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * tempGun.shootPower, ForceMode2D.Impulse);
            bullet.transform.localScale = tempGun.bulletSize;
            bullet.GetComponent<Renderer>().material.SetColor("_EmissionColor", tempGun.bulletColor);
            bullet.GetComponent<ParticleSystem>().startColor = tempGun.bulletColor;
            base.OnGunShoot();
        }

    }
}
