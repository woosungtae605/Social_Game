using DevLib.EventChannelSystem;
using UnityEngine;

namespace DevLib.SoundSystem
{
    public static class SoundEvents
    {
        public const string MasterVolumePrefKey = "Sound.MasterVolume";

        public static readonly PlaySoundEvent PlaySoundEvent = new PlaySoundEvent();
        public static readonly StopSoundEvent StopSoundEvent = new StopSoundEvent();
        public static readonly SetSoundVolumeEvent SetSoundVolumeEvent = new SetSoundVolumeEvent();
    }

    public class PlaySoundEvent : GameEvent
    {
        public Vector3 Position;
        public SoundClipSo ClipData;
        public int ChannelNumber;

        public PlaySoundEvent Init(Vector3 position, SoundClipSo clipData, int channelNumber = 0)
        {
            Position = position;
            ClipData = clipData;
            ChannelNumber = channelNumber;
            return this;
        }
    }

    public class StopSoundEvent : GameEvent
    {
        public int ChannelNumber;

        public StopSoundEvent Init(int channelNumber = 0)
        {
            ChannelNumber = channelNumber;
            return this;
        }
    }

    public class SetSoundVolumeEvent : GameEvent
    {
        public float Volume;

        public SetSoundVolumeEvent Init(float volume)
        {
            Volume = volume;
            return this;
        }
    }
}