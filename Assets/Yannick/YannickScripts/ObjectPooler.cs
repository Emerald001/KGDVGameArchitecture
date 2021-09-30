using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{
    public ObjectPooler(string _tag, GameObject _prefab, int _size)
    {
        this.tag = _tag;
        this.prefab = _prefab;
        this.size = _size;
    }

    public string tag;
    public GameObject prefab;
    public int size;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public void OnStart()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        Queue<GameObject> objectPool = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        poolDictionary.Add(tag, objectPool);
    }

    public GameObject SpawnFromPool(string _tag, Vector3 _position)
    {
        if (!poolDictionary.ContainsKey(_tag))
        {
            Debug.Log("error");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[_tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = _position;

        poolDictionary[_tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}