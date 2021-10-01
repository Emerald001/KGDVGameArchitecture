using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AutoFireModifier", order = 0)]

public class AutoFireDecorator : GunModifier
{
    public override void OnGunStart()
    {
        tempGun.autoFire = true;
        base.OnGunStart();

    }
}
