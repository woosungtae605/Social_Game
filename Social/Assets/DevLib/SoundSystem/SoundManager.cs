using System.Collections.Generic;
using DevLib.EventChannelSystem;
using DevLib.ObjectPool.Runtime;
using UnityEngine;

namespace DevLib.SoundSystem
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private PoolItemSO soundItem;

        [field: SerializeField] public EventChannelSO SoundChannel { get; private set; }

        private readonly Dictionary<int, SoundPlayer> _soundPlayerDict = new();

        private void Awake()
        {
            
            SoundManager[] managers = FindObjectsByType<SoundManager>(FindObjectsSortMode.None);
            if (managers.Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);

            SoundChannel.AddListener<PlaySoundEvent>(HandlePlaySoundEvent);
            SoundChannel.AddListener<StopSoundEvent>(HandleStopSoundEvent);
            SoundChannel.AddListener<SetSoundVolumeEvent>(HandleSetVolumeEvent);

            ApplyMasterVolume(PlayerPrefs.GetFloat(SoundEvents.MasterVolumePrefKey, 1f), persist: false);
        }

        private void OnDestroy()
        {
            if (SoundChannel == null)
                return;

            SoundChannel.RemoveListener<PlaySoundEvent>(HandlePlaySoundEvent);
            SoundChannel.RemoveListener<StopSoundEvent>(HandleStopSoundEvent);
            SoundChannel.RemoveListener<SetSoundVolumeEvent>(HandleSetVolumeEvent);
        }

        private void HandleSetVolumeEvent(SetSoundVolumeEvent evt)
        {
            ApplyMasterVolume(evt.Volume, persist: true);
        }

        private static void ApplyMasterVolume(float volume, bool persist)
        {
            float clamped = Mathf.Clamp01(volume);
            AudioListener.volume = clamped;

            if (!persist)
                return;

            PlayerPrefs.SetFloat(SoundEvents.MasterVolumePrefKey, clamped);
            PlayerPrefs.Save();
        }

        private void HandlePlaySoundEvent(PlaySoundEvent evt)
        {
            if (evt.ClipData == null || evt.ClipData.clip == null)
                return;

            SoundPlayer player = poolManager.Pop<SoundPlayer>(soundItem);
            if (player == null)
                return;
            player.transform.position = evt.Position;
            player.PlaySound(evt.ClipData);
            player.OnSoundFinished += HandleSoundFinish;

            if (evt.ChannelNumber > 0 && evt.ClipData.loop)
            {
                if (_soundPlayerDict.TryGetValue(evt.ChannelNumber, out SoundPlayer beforePlayer))
                {
                    beforePlayer.ForceStopSound();
                    beforePlayer.OnSoundFinished -= HandleSoundFinish;
                    poolManager.Push(beforePlayer);
                    _soundPlayerDict.Remove(evt.ChannelNumber);
                }

                _soundPlayerDict.Add(evt.ChannelNumber, player);
            }
            else if (evt.ChannelNumber <= 0 && evt.ClipData.loop)
            {
                Debug.LogWarning(
                    $"Channel must be greater than 0, when the Sound data loop is enabled : {evt.ClipData.name}");
            }
        }

        private void HandleSoundFinish(SoundPlayer player)
        {
            player.OnSoundFinished -= HandleSoundFinish;
            poolManager.Push(player);
        }


        private void HandleStopSoundEvent(StopSoundEvent evt)
        {
            if (_soundPlayerDict.TryGetValue(evt.ChannelNumber, out SoundPlayer beforePlayer))
            {
                beforePlayer.ForceStopSound();
                beforePlayer.OnSoundFinished -= HandleSoundFinish;
                poolManager.Push(beforePlayer);
                _soundPlayerDict.Remove(evt.ChannelNumber);
            }
        }
    }
}