public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size; 
    }

    #region Singleton

    public static ObjectPooler Instance;
//    // Start is called before the first frame update
//    void OnStart()
//    {
//        poolDictionary = new Dictionary<string, Queue<T>>();

    #endregion


//            for (int i = 0; i < pool.totalSize; i++)
//            {
//                T obj = new prefab;
//                objectPool.Enqueue(obj);
//            }

//            poolDictionary.Add(pool.tag, objectPool);
//        }
//    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();


//        T objectToSpawn = poolDictionary[_tag].Dequeue();


            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj =  Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);

        }
        
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {

        Debug.Log("Dit werkt ");
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("error");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}

