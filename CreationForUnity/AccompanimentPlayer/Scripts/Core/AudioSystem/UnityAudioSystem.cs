using System;
using MusicPlayer.Setting;
using UnityEngine;
using UniRx;

namespace MusicPlayer.Core.AudioSystem
{
    public class UnityAudioSystem : IAudioSystem 
    {
        private MusicEntity entity;
        private AudioSource audioSource;
        private bool isPause = false;
        private readonly Subject<float> audioSequenceSubject = new Subject<float>();
        private readonly Subject<MusicEntity> audioStartedSubject = new Subject<MusicEntity>();
        private readonly Subject<Unit> audioFinishedSubject = new Subject<Unit>();
        private readonly Subject<bool> audioPlayingSubject = new Subject<bool>();
        private MultipleAssignmentDisposable multipleDisposable = new MultipleAssignmentDisposable();

        public IObservable<float> OnMusicSequenceChanged => audioSequenceSubject;
        public IObservable<MusicEntity> OnMusicStarted => audioStartedSubject;
        public IObservable<Unit> OnMusicFinished => audioFinishedSubject;

        public IObservable<bool> OnPlayingMusic => audioPlayingSubject;

        public float Volume => audioSource.volume;
        public float Pitch => audioSource.pitch;

        public float Sequence {
            get
            {
                if (audioSource == null || entity == null)
                {
                    return 0.0f;
                }
                return Mathf.Clamp01(audioSource.time / entity.Music.length);
            }
        }

        public void Setup(MonoBehaviour behaviour,AudioSystemSetting setting)
        {
            multipleDisposable = new MultipleAssignmentDisposable();
            
            if (this.audioSource == null)
            {
                this.audioSource = behaviour.gameObject.AddComponent<AudioSource>();
            }

            SetVolume(setting.Volume);
            SetPitch(setting.Pitch);

            multipleDisposable.Disposable = Observable.EveryUpdate()
                .Where(_ => audioSource != null)
                .Subscribe(_ =>
            {
                var sequence = Sequence;
                audioSequenceSubject.OnNext(sequence);
                if (sequence >= 1.0f)
                {
                    finishedPlayMusic();
                }
            });
        }

        public void SetMusicEntity(MusicEntity entity)
        {
            this.entity = entity;
            audioSource.clip = entity.Music;
        }
        
        
        public void PlayMusic(MusicEntity entity = null)
        {
            if (entity != null)
            {
                SetMusicEntity(entity);
            }

            if (isPause)
            {
                audioSource.UnPause();
            }
            else
            {
                audioSource.Play();
                audioStartedSubject.OnNext(entity);
            }

            isPause = false;
            
            audioPlayingSubject.OnNext(true);
        }

        public void PauseMusic()
        {
            if (!audioSource.isPlaying)
            {
                return;
            }
            audioSource.Pause();
            audioPlayingSubject.OnNext(false);
            isPause = true;
        }

        public void StopMusic()
        {
            if (!audioSource.isPlaying)
            {
                return;
            }
            audioSource.Stop();
            finishedPlayMusic();
        }

        private void finishedPlayMusic()
        {
            SetMusicSequence(0.0f);
            audioPlayingSubject.OnNext(false);
            audioFinishedSubject.OnNext(Unit.Default);
            isPause = false;
        }

        public void RewindMusic()
        {
            SetMusicSequence(0.0f);
        }

        public void FastForwardMusic()
        {
            SetMusicSequence(1.0f);
        }

        public void SetMusicSequence(float value)
        {
            value = Mathf.Clamp(value,0.0f,0.999f);//1.0で指定するとエラーを起こすので少し下げる
            audioSource.time = entity.Music.length * value;
        }

        public void SetVolume(float value)
        {
            audioSource.volume = value;
        }

        public void SetPitch(float value)
        {
            audioSource.pitch = value;
        }

        public void Dispose()
        {
            GameObject.Destroy(audioSource);
            audioSequenceSubject.Dispose();
            audioStartedSubject.Dispose();
            audioFinishedSubject.Dispose();
            audioPlayingSubject.Dispose();
            multipleDisposable.Dispose();
        }
    }
}
