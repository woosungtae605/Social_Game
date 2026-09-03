using DevLib.EventChannelSystem;
using DevLib.SoundSystem;
using UnityEngine;

namespace Team.WST.Scripts.Title
{
    public class TitleBgmPlayer : MonoBehaviour
    {
        [SerializeField] private EventChannelSO soundChannel;
        [SerializeField] private SoundClipSo bgmClip;
        [SerializeField] private int channelNumber = 1;

        private void Start()
        {
            Play();
        }

        private void OnDestroy()
        {
            Stop();
        }

        private void Play()
        {
            if (soundChannel == null || bgmClip == null)
                return;

            soundChannel.RaiseEvent(SoundEvents.PlaySoundEvent.Init(Vector3.zero, bgmClip, channelNumber));
        }

        private void Stop()
        {
            if (soundChannel == null)
                return;

            soundChannel.RaiseEvent(SoundEvents.StopSoundEvent.Init(channelNumber));
        }
    }
}
