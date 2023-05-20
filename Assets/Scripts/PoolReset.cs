using UnityEngine;
using System.Collections;

namespace ObjectPool
{
    public class PoolReset : MonoBehaviour
    {
        public virtual void Reset() { }

        public void ResetAll()
        {
            Reset();
            PoolReset[] childs = GetComponentsInChildren<PoolReset>();
            foreach (PoolReset toReset in childs)
                toReset.Reset();
        }

        protected void Deactive()
        {
            if (!GetComponent<PoolObject>())
                gameObject.AddComponent<PoolObject>();

            GetComponent<PoolObject>().ReturnToPool();
        }

        protected void Deactive(float time)
        {
            StartCoroutine(DeactiveCo(time));
        }

        IEnumerator DeactiveCo(float t)
        {
            yield return new WaitForSeconds(t);
            Deactive();
        }
    }
}
