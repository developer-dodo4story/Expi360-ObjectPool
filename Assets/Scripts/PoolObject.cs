using UnityEngine;

namespace ObjectPool
{
    public class PoolObject : MonoBehaviour
    {
        bool returnDisabledObject = true;
        Transform parent;

        public void SetParent(Transform parent)
        {
            this.parent = parent;
        }

        public void ReturnToPool()
        {
            if (returnDisabledObject)
            {
                transform.parent = parent;
                GetComponent<PoolReset>()?.Reset();
                gameObject.SetActive(false);
            }
        }

        void DisableReturn()
        {
            returnDisabledObject = false;
        }

        private void OnApplicationQuit()
        {
            DisableReturn();
        }

        void OnDisable()
        {
            //ReturnToPool();
        }
    }
}
