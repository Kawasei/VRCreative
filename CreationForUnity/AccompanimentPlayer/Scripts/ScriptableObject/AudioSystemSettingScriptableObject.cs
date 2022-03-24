using System;
using UniRx;
using UnityEngine;

namespace MusicPlayer.Setting
{
    public enum AudioSystemTypes
    {
        Unity,
    }
    
    [CreateAssetMenu(menuName = "MusicPlayer/Create MusicPlayerSetting")]
    public class AudioSystemSettingScriptableObject : ScriptableObject
    {
        public AudioSystemTypes AudioSystemType = AudioSystemTypes.Unity;
        
        public float Volume = 1.0f;
        public float Pitch = 1.0f;

        public bool IsAutoNextMusic = false;
        public bool IsShuffle = false;
    }

    public class AudioSystemSetting
    {
        private readonly ReactiveProperty<AudioSystemTypes> audioSystemType;
        public AudioSystemTypes AudioSystemType => audioSystemType.Value;
        public IObservable<AudioSystemTypes> OnChangedAudioSystemType => audioSystemType;

        private readonly ReactiveProperty<float> volume;
        public float Volume => volume.Value;
        public IObservable<float> OnChangedVolume => volume;

        private readonly ReactiveProperty<float> pitch;
        public float Pitch => pitch.Value;
        public IObservable<float> OnChangedPitch => pitch;

        private readonly ReactiveProperty<bool> isAutoNextMusic;
        public bool IsAutoNextMusic => isAutoNextMusic.Value;
        public IObservable<bool> OnChangedIsAutoNextMusic => isAutoNextMusic;

        private readonly ReactiveProperty<bool> isShuffle;
        public bool IsShuffle => isShuffle.Value;
        public IObservable<bool> OnChangedIsShuffle => isShuffle;

        public AudioSystemSetting(AudioSystemSettingScriptableObject scriptableObject)
        {
            audioSystemType = new ReactiveProperty<AudioSystemTypes>(scriptableObject.AudioSystemType);
            volume = new ReactiveProperty<float>(scriptableObject.Volume);
            pitch = new ReactiveProperty<float>(scriptableObject.Pitch);
            isAutoNextMusic = new ReactiveProperty<bool>(scriptableObject.IsAutoNextMusic);
            isShuffle = new ReactiveProperty<bool>(scriptableObject.IsShuffle);
        }
    }
}