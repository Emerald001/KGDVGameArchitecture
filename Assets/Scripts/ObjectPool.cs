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
        if (inactivePool.Count > 0)
        {
            return ActivateItem(inactivePool[0]);
        }
        return ActivateItem(AddNewItemToPool());
    }

    public T ActivateItem(T _item)
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
        T instance = (T)Activator.CreateInstance(typeof(T));
        inactivePool.Add(instance);
        return instance;
    }

    public void RunFixedUpdate()
    {
        for (int i = 0; i < activePool.Count; i++)
        {
            activePool[i].OnFixedUpdate();
        }
    }
}
