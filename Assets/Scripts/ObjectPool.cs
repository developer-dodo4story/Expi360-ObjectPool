using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        public enum TypeOfPool { CONSTANT, EXPANDING };

        [SerializeField]
        TypeOfPool typeOfPool = TypeOfPool.CONSTANT;

        [SerializeField]
        GameObject source;

        [SerializeField]
        int poolCount;

        int objNumber = 0;

        List<GameObject> pool = new List<GameObject>();
        Queue<GameObject> poolQueue = new Queue<GameObject>();

        // Use this for initialization
        void Awake()
        {
            InitPool();
        }

        public void InitPool()
        {
            //wypełnia listę kopiami obiektu podanego w source
            switch (typeOfPool)
            {
                case TypeOfPool.CONSTANT:
                    for (int i = 0; i < poolCount; i++)
                    {
                        GameObject obj = (GameObject)Instantiate(source);
                        obj.SetActive(false);
                        poolQueue.Enqueue(obj);
                        obj.transform.parent = transform;
                        if (!obj.GetComponent<PoolObject>())
                            obj.AddComponent<PoolObject>();
                        obj.GetComponent<PoolObject>().SetParent(transform);
                        obj.name = source.name + "_" + objNumber;
                        objNumber++;
                    }
                    Destroy(source);
                    break;

                case TypeOfPool.EXPANDING:
                    pool.Add(source);
                    if (!source.GetComponent<PoolObject>())
                        source.AddComponent<PoolObject>();
                    source.GetComponent<PoolObject>().SetParent(transform);

                    for (int i = 0; i < poolCount - 1; i++)
                    {
                        GameObject obj = (GameObject)Instantiate(source);
                        obj.SetActive(false);
                        pool.Add(obj);
                        obj.transform.parent = transform;
                        if (!obj.GetComponent<PoolObject>())
                            obj.AddComponent<PoolObject>();
                        obj.GetComponent<PoolObject>().SetParent(transform);
                        obj.name = source.name + "_" + objNumber;
                        objNumber++;
                    }
                    //Destroy(source);
                    break;
                default:
                    break;
            }

        }

        public GameObject GetPoolObject()
        {
            GameObject obj;

            switch (typeOfPool)
            {
                case TypeOfPool.CONSTANT:
                    //zwraca najstarszy obiekt
                    //(rozwiązanie właściwe np dla particli)
                    obj = poolQueue.Dequeue();
                    poolQueue.Enqueue(obj);
                    break;
                case TypeOfPool.EXPANDING:
                    //zwraca najstarszy obiekt, jeśli jest aktywny to tworzy nowy
                    //(rozwiązanie właściwe np dla przeciwników)
                    for (int i = 0; i < pool.Count; i++)
                    {
                        if (!pool[i].activeInHierarchy)
                        {
                            return pool[i];
                        }
                    }
                    obj = (GameObject)Instantiate(source);
                    obj.SetActive(false);
                    pool.Add(obj);
                    obj.transform.parent = transform;
                    if (!obj.GetComponent<PoolObject>())
                        obj.AddComponent<PoolObject>();
                    obj.GetComponent<PoolObject>().SetParent(transform);
                    obj.name = source.name + "_" + objNumber;
                    objNumber++;
                    break;
                default:
                    obj = null;
                    break;
            }
            return obj;
        }
    }
}
