using UnityEngine;

public class CubeSpawner : MonoBehaviour, IPooledObject
{
    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        OnObjectSpawn();
    }
    public void OnObjectSpawn()
    {
        objectPooler.SpawnFromPool("Cube", transform.position, Quaternion.identity);

    }
}

