using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YannickManager : MonoBehaviour
{
    public ObjectPooler objectpooler;
    public GameObject spawnerPrefab;

    [SerializeField] private int size;

    // Start is called before the first frame update
    void Start()
    {
        objectpooler = new ObjectPooler("Cube", spawnerPrefab, size);
        objectpooler.OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        objectpooler.SpawnFromPool("Cube", transform.position, transform.rotation);
    }
}
