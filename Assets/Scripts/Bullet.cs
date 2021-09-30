using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : IPoolable
{
    bool IPoolable.Active { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void IPoolable.OnDisableObject()
    {
        throw new System.NotImplementedException();
    }

    void IPoolable.OnEnableObject()
    {
        throw new System.NotImplementedException();
    }
}
