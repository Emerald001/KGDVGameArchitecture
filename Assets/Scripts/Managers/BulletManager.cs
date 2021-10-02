using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager
{
    public List<Bullet> bullets;

    public BulletManager()
    {
        bullets = new List<Bullet>();
    }

    public void OnStart()
    {

    }

    public void OnFixedUpdate()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].OnFixedUpdate();
        }
    }



}
