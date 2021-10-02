using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ExplosiveModifier", order = 1)]
public class ExplosionModifier : GunModifier
{
    public override void OnGunStart()
    {
        tempGun.isExplosive = true;
        base.OnGunStart();
    }

}
