#pragma warning disable 0414

using UnityEngine;
using Scripts.ScriptableObjects;

namespace Scripts.Utilities
{
    public enum SoundType
    {
        Master,
        BGM,
        SE
    }

    public static class SoundExtension
    {
        private static readonly float MinVolume = -20f;
        private static readonly float MaxVolume = 20f;

        private static string GetAMGroupString(SoundType soundType)
        {
            return soundType switch
            {
                SoundType.Master => "MasterParam",
                SoundType.BGM => "BGMParam",
                SoundType.SE => "SEParam",
                _ => string.Empty
            };
        }

        public static float GetVolume(SoundType soundType, out bool muted)
        {
            SSound.Entity.AudioMixer.GetFloat(GetAMGroupString(soundType), out float volume);
            muted = volume <= MinVolume;
            return volume;
        }

        public static void SetVolume(SoundType soundType, float newVolume, out bool muted)
        {
            muted = false;
            if (newVolume <= MinVolume)
            {
                newVolume = -80;
                muted = true;
            }

            SSound.Entity.AudioMixer.SetFloat(GetAMGroupString(soundType), newVolume);
        }

        public static void Raise
            (this AudioSource source, AudioClip clip, SoundType type, float volume = 1, float pitch = 1, float time = 0)
        {
            if (source == null) return;
            if (clip == null) return;

            source.playOnAwake = false;

            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            source.time = time;

            if (type == SoundType.BGM)
            {
                source.outputAudioMixerGroup = SSound.Entity.AMGroupBGM;
                source.loop = true;
            }
            else if (type == SoundType.SE)
            {
                source.outputAudioMixerGroup = SSound.Entity.AMGroupSE;
                source.loop = false;
            }
            else
                return;

            source.Play();
        }
    }
}