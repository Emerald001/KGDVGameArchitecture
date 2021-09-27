using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour, IPooledObject
{
    public GameObject cubePrefab;

    public void OnObjectSpawn()
    {
        ObjectPooler.Instance.SpawnFromPool("Cube", transform.position, Quaternion.identity);
    }
}
