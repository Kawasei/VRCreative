using System;
using MusicPlayer.Setting;
using UniRx;
using UnityEngine;

namespace MusicPlayer.Core.AudioSystem
{
    public interface IAudioSystem : IDisposable
    {
        IObservable<float> OnMusicSequenceChanged { get; }
        IObservable<MusicEntity> OnMusicStarted { get; }
        IObservable<Unit> OnMusicFinished { get; }
        
        IObservable<bool> OnPlayingMusic { get; }

        float Volume { get; }
        float Pitch { get; }
        float Sequence { get; }

        void Setup(MonoBehaviour behaviour, AudioSystemSetting setting);
        
        void SetMusicEntity(MusicEntity entity);

        void PlayMusic(MusicEntity entity = null);
        void PauseMusic();
        void StopMusic();
        void RewindMusic();
        void FastForwardMusic();

        void SetMusicSequence(float value);

        void SetVolume(float value);

        void SetPitch(float value);
    }
}


