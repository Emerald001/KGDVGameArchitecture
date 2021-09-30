using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour, IPooledObject
{
    ObjectPooler objectPooler;

    private void Start()
    {
        
    }

    private void Update()
    {
        OnObjectSpawn();
    }
    public void OnObjectSpawn()
    {
        objectPooler.SpawnFromPool("Cube", transform.position);

    }
}