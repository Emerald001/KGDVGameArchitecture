using System;
using UnityEngine;
using System.Collections.Generic;
using Interfaces;

public class ObjectPool<T> where T : IPoolable
{
    private List<T> activePool = new List<T>();
    private List<T> inactivePool = new List<T>();

    public ObjectPool()
    {
        AddNewItemToPool();
    }

    public T RequestObject()
    {
        return ActivateItem(inactivePool.Count > 0 ? inactivePool[0] : AddNewItemToPool());
    }

    private T ActivateItem(T _item)
    {
        _item.OnEnableObject();
        if (inactivePool.Contains(_item))
        {
            inactivePool.Remove(_item);
        }
        activePool.Add(_item);
        return _item;
    }

    public void ReturnObjectToPool(T _item)
    {
        if (activePool.Contains(_item))
        {
            activePool.Remove(_item);
        }
        _item.OnDisableObject();
        inactivePool.Add(_item);
    }

    private T AddNewItemToPool()
    {
        var instance = (T)Activator.CreateInstance(typeof(T));
        inactivePool.Add(instance);
        return instance;
    }

    public void RunFixedUpdate()
    {
        for (var i = 0; i < activePool.Count; i++)
        {
            var t = activePool[i];
            t.OnFixedUpdate();
        }
    }
}
