using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunModifier : ScriptableObject
{
    public Gun tempGun; 

    // Start is called before the first frame update
    public int bulletDamage
    {
        get;
        set;
    }


    public virtual void BulletUpdate()
    {

    }

    public virtual void OnGunShoot()
    {
        // Debug.Log("bullet shot called in Abstract Base Modifier");
        //gunParent = _gunParent;
    }

    public virtual void OnGunStart()
    {

    }
}
