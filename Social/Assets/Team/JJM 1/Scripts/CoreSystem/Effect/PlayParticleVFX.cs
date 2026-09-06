using UnityEngine;

namespace JJM.Scripts.CoreSystem.Effect
{
    public class PlayParticleVFX : MonoBehaviour, IPlayableVFX
    {
        [field: SerializeField]
        public AssetNameSo VfxName { get; private set; }

        [field: SerializeField]
        public float VfxDuration { get; private set; }

        [SerializeField] private ParticleSystem[] particles;

        public void PlayVFX(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
            PlayVFX();
        }

        public void PlayVFX()
        {
            foreach (ParticleSystem particle in particles)
            {
                if (particle == null)
                    continue;

                particle.Stop(
                    true,
                    ParticleSystemStopBehavior.StopEmittingAndClear
                );

                particle.Play(true);
            }
        }

        public void StopVFX()
        {
            foreach (ParticleSystem particle in particles)
            {
                if (particle == null)
                    continue;

                particle.Stop(
                    true,
                    ParticleSystemStopBehavior.StopEmittingAndClear
                );
            }
        }
    }
}