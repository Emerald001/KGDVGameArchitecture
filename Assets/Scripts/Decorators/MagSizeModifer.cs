using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MagSizeModifier", order = 1)]

public class MagSizeModifer : GunModifier
{
    // Start is called before the first frame update

    public int ExtraMagSize;
    public override void OnGunStart()
    {
        tempGun.magSize += ExtraMagSize;
        base.OnGunStart();
    }
}
