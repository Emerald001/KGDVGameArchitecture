//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ObjectPooler<T>
//{
//    [System.Serializable]
//    public class Pool
//    {
//        public string tag;
//        public T prefab;
//        public int totalSize;
//    }

//    public List<Pool> pools;
//    public Dictionary<string, Queue<T>> poolDictionary;


//    // Start is called before the first frame update
//    void OnStart()
//    {
//        poolDictionary = new Dictionary<string, Queue<T>>();

//        foreach(Pool pool in pools)
//        {
//            Queue<T> objectPool = new Queue<T>();

//            for (int i = 0; i < pool.totalSize; i++)
//            {
//                T obj = new prefab;
//                objectPool.Enqueue(obj);
//            }

//            poolDictionary.Add(pool.tag, objectPool);
//        }
//    }

//    public T SpawnFromPool(string _tag, Vector3 _position, Quaternion _rotation)
//    {
//        if (!poolDictionary.ContainsKey(_tag))
//        {
//            return null;
//        }

//        T objectToSpawn = poolDictionary[_tag].Dequeue();

//        objectToSpawn.SetActive(true);
//        objectToSpawn.transform.position = _position;
//        objectToSpawn.transform.rotation = _rotation;


//        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>(); 
//        if (pooledObject != null)
//        {
//            pooledObject.OnObjectSpawn();
//        }

//        poolDictionary[_tag].Enqueue(objectToSpawn);

//        return objectToSpawn;
//    }
//}
