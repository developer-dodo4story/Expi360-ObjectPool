using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class ProjectorPoolReset : PoolReset
    {
        [SerializeField]
        float deactiveTime = 3f;

        private void OnEnable()
        {
            Deactive(deactiveTime);
        }
    }
}
