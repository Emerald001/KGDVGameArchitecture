using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    void FixedUpdate()
    {
        ObjectPooler.instance.SpawnFromPool("Cube", transform.position, Quaternion.identity);
    }
}
