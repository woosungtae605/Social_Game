using DevLib.EventChannelSystem;
using DevLib.SoundSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Team.WST.Scripts.Title
{
    public class TitleSoundCue : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        [SerializeField] private EventChannelSO soundChannel;
        [SerializeField] private SoundClipSo hoverClip;
        [SerializeField] private SoundClipSo clickClip;

        public void OnPointerEnter(PointerEventData eventData)
        {
            Play(hoverClip);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Play(clickClip);
        }

        private void Play(SoundClipSo clip)
        {
            if (soundChannel == null || clip == null)
                return;

            soundChannel.RaiseEvent(SoundEvents.PlaySoundEvent.Init(Vector3.zero, clip));
        }
    }
}
