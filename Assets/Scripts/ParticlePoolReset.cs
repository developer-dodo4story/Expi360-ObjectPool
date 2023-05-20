using UnityEngine;

namespace ObjectPool
{
    public class ParticlePoolReset : PoolReset
    {
        ParticleSystem[] particleSystems;

        public override void Reset()
        {
            particleSystems = GetComponents<ParticleSystem>();
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Clear();
                ps.Play();
            }
        }
    }
}
