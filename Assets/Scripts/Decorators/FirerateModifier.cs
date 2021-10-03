using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FireRateModifier", order = 1)]
public class FirerateModifier : GunModifier
{
    public float FireRatemultiplier;

    public override void OnGunStart()
    {
        tempGun.fireRate *= FireRatemultiplier;
        base.OnGunStart();
    }
}