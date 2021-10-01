using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BulletSizeModifier", order = 1)]
public class BulletSizeModifier : GunModifier
{
    public float bulletSizeMultiplier;

    public override void OnGunStart()
    {
        Debug.Log("Bulletsize ongunstart");
        tempGun.bulletSize *= bulletSizeMultiplier;
        base.OnGunStart();
    }
}
