using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ShootPowerModifier", order = 1)]

public class ShootPowerModifier : GunModifier
{
    public float extraShootPower;

    public override void OnGunStart()
    {
        tempGun.shootPower += extraShootPower;
        base.OnGunStart();
    }
}
