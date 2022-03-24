using System;
using MusicPlayer.Core.AudioSystem;
using MusicPlayer.Setting;
using UnityEngine;
using UniRx;
using Random = UnityEngine.Random;

namespace MusicPlayer.Core.Controller
{
    public class MusicPlayerController : MonoBehaviour
    {
        private const float playAnotherMusicSequenceBorder = 0.02f;

        [SerializeField] private MusicListScriptableObject musicList;
        [SerializeField] private AudioSystemSettingScriptableObject settingObject;
        
        private AudioSystemSetting setting;
        private int? selectedMusicEntityIndex;
        private IAudioSystem audioSystem;
        private readonly MultipleAssignmentDisposable multipleDisposable = new MultipleAssignmentDisposable();
        private readonly Subject<Unit> onFinishedMusicSubject = new Subject<Unit>();
        
        private bool isExistAudioSystem => audioSystem != null;

        public IObservable<float> OnMusicSequenceChanged => audioSystem.OnMusicSequenceChanged;
        public IObservable<MusicEntity> OnMusicStarted => audioSystem.OnMusicStarted;
        public IObservable<Unit> OnMusicFinished => onFinishedMusicSubject;
        public IObservable<bool> OnPlayingMusic => audioSystem.OnPlayingMusic;

        private void Awake()
        {
            setting = new AudioSystemSetting(settingObject);

            audioSystem = AudioSystemFactory.CreateAudioSystem(this, setting);

            multipleDisposable.Disposable = audioSystem.OnMusicFinished
                .Subscribe(_ =>
                {
                    if (!setting.IsAutoNextMusic)
                    {
                        onFinishedMusicSubject.OnNext(Unit.Default);
                        return;
                    }

                    selectedMusicEntityIndex = setting.IsShuffle ? getShuffleMusicIndex() : getNextMusicIndex();
                    PlayMusic();

                });
        }

        #region MusicPlay
        public void PlayMusic()
        {
            if (!isExistAudioSystem)
            {
                return;
            }
            
            if (selectedMusicEntityIndex == null && musicList.Musics.Count != 0)
            {
                selectedMusicEntityIndex = 0;
            }

            if (selectedMusicEntityIndex == null)
            {
                Debug.LogError("再生可能な楽曲がありません");
                return;
            }
            
            audioSystem.PlayMusic(musicList.Musics[selectedMusicEntityIndex.Value]);
        }

        public void PauseMusic()
        {
            if (!isExistAudioSystem)
            {
                return;
            }
            
            audioSystem.PauseMusic();
        }
        
        public void StopMusic()
        {
            if (!isExistAudioSystem)
            {
                return;
            }
            
            audioSystem.StopMusic();
        }

        public void SetMusicSequence(float value)
        {
            audioSystem.SetMusicSequence(value);
        }
        
        public void RewindMusic()
        {
            if (!isExistAudioSystem)
            {
                return;
            }
            
            if ((audioSystem?.Sequence ?? 1.0f) <= playAnotherMusicSequenceBorder)
            {
                selectedMusicEntityIndex = setting.IsShuffle ? getShuffleMusicIndex() : getPrevMusicIndex();
                PlayMusic();
            }
            else
            {
                audioSystem?.RewindMusic();
            }
        }

        public void FastForwardMusic()
        {
            if ((audioSystem?.Sequence ?? 0.0f) >= (1.0f - playAnotherMusicSequenceBorder))
            {
                selectedMusicEntityIndex = setting.IsShuffle ? getShuffleMusicIndex() : getNextMusicIndex();
                PlayMusic();
            }
            else
            {
                audioSystem?.FastForwardMusic();
            }
        }
        
        private int getPrevMusicIndex()
        {
            if (!selectedMusicEntityIndex.HasValue)
            {
                return musicList.Musics.Count - 1;
            }
            
            return (selectedMusicEntityIndex.Value + musicList.Musics.Count - 1) % musicList.Musics.Count;
        }

        private int getNextMusicIndex()
        {
            if (!selectedMusicEntityIndex.HasValue)
            {
                return 0;
            }
            
            return (selectedMusicEntityIndex.Value + 1) % musicList.Musics.Count;
        }

        private int getShuffleMusicIndex()
        {
            var index = 0;
            while (true)
            {
                index = Random.Range(0, musicList.Musics.Count);
                if (!selectedMusicEntityIndex.HasValue || index != selectedMusicEntityIndex.Value)
                {
                    break;
                }
            }

            return index;
        }
#endregion
        private void OnDestroy()
        {
            if (multipleDisposable != null)
            {
                multipleDisposable.Dispose();
            }
        }
    }
}


