using UnityEngine;

namespace ObjectPool
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public GameObject SpawnPoolObject(string name, Vector3 worldPosition)
        {
            //pobiera obiekt z puli o podanej nazwie, aktywuje i umieszcza w pozycji worldpos
            Transform poolTransform = transform.Find(name);
            if (poolTransform)
            {
                GameObject poolObject = poolTransform.gameObject.GetComponent<ObjectPool>().GetPoolObject();
                if (poolObject)
                {
                    if (poolObject.GetComponent<PoolReset>())
                    {
                        poolObject.GetComponent<PoolReset>().ResetAll();
                        poolObject.SetActive(false);

                        poolObject.transform.position = worldPosition;
                        poolObject.SetActive(true);
                    }
                    else
                        Debug.LogWarning("ObjectPoolManager: SpawnPoolObject(): No 'PoolReset' component in '" + poolObject + "'");

                    return poolObject;
                }
                else
                    Debug.LogWarning("ObjectPoolManager: SpawnPoolObject(): No 'ObjectPool' component in '" + poolObject + "'");
            }
            else
                Debug.LogWarning("ObjectPoolManager: SpawnPoolObject(): No 'Pool' with name: '" + name + "'");

            return null;
        }

        public GameObject SpawnPoolObject(string name, Vector3 worldPosition, Transform parent)
        {
            GameObject poolObject = SpawnPoolObject(name, worldPosition);
            if (poolObject)
            {
                poolObject.transform.parent = parent;
            }
            return poolObject;
        }

        public GameObject SpawnPoolObject(string name, Vector3 worldPosition, Vector3 eulerAngles)
        {
            GameObject poolObject = SpawnPoolObject(name, worldPosition);
            if (poolObject)
            {
                poolObject.transform.eulerAngles = eulerAngles;
            }
            return poolObject;
        }
    }
}
