using System;
using MusicPlayer.Core.Controller;
using UsefulCalsses;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MusicPlayer.ControlPanel.View
{
    public class UIMusicPlayerControlPanelView : AbstractMusicPlayerControlPanelView
    {
        [SerializeField] private Button playButton;
        public override IObservable<Unit> OnClickPlayButton => playButton.onClick.AsObservable();

        [SerializeField] private Button pauseButton;
        public override IObservable<Unit> OnClickPauseButton => pauseButton.onClick.AsObservable();

        [SerializeField] private Button stopButton;
        public override IObservable<Unit> OnClickStopButton => stopButton.onClick.AsObservable();

        [SerializeField] private Button fastForwardButton;
        public override IObservable<Unit> OnClickFastForwardButton => fastForwardButton.onClick.AsObservable();

        [SerializeField] private Button rewindButton;
        public override IObservable<Unit> OnClickRewindButton => rewindButton.onClick.AsObservable();

        [SerializeField] private Slider sequenceSlider;
        private readonly Subject<float> sequenceSliderSubject = new Subject<float>();
        public override IObservable<float> OnChangedMusicSequenceByUser => sequenceSliderSubject;

        [SerializeField] private Button closeButton;
        public override IObservable<Unit> OnClickCloseButton => closeButton.onClick.AsObservable();

        [SerializeField] private Button openSelectorButton;
        public override IObservable<Unit> OnClickOpenSelectorButton => openSelectorButton.onClick.AsObservable();

        [SerializeField] private Button lyricButton;
        public override IObservable<Unit> OnClickLyricButton => lyricButton.onClick.AsObservable();

        [SerializeField] private Button settingButton;
        public override IObservable<Unit> OnClickSettingButton => settingButton.onClick.AsObservable();

        [SerializeField] private Text musicInfoText;

        [SerializeField] private WorldObjectPositionAnchor worldObjectPositionAnchor;

        private bool isSliderChangedByMethod;

        public override void Setup(MusicPlayerController controller,GameObject anchorObject)
        {
            OnClickPlayButton.Subscribe(_ => controller.PlayMusic()).AddTo(this);
            OnClickPauseButton.Subscribe(_ => controller.PauseMusic()).AddTo(this);
            OnClickStopButton.Subscribe(_ => controller.StopMusic()).AddTo(this);
            OnClickRewindButton.Subscribe(_ => controller.RewindMusic()).AddTo(this);
            OnClickFastForwardButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);
            OnChangedMusicSequenceByUser.Subscribe(value => controller.SetMusicSequence(value)).AddTo(this);
            //view.OnClickLyricButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);
            //view.OnClickOpenSelectorButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);
            //view.OnClickSettingButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);
            OnClickCloseButton.Subscribe(_ => Destroy(this.gameObject)).AddTo(this);

            controller.OnMusicSequenceChanged.Subscribe(value => UpdateMusicSequence(value)).AddTo(this);
            controller.OnMusicStarted.Subscribe(musicEntity =>
                    SetMusicInfo(musicEntity.Title, musicEntity.Singer, musicEntity.LyricsWriter, musicEntity.Composer))
                .AddTo(this);
            controller.OnMusicFinished.Subscribe(_ =>
                SetMusicInfo(null, null, null, null)).AddTo(this);
            controller.OnPlayingMusic.Subscribe(isPlaying => ActivePauseButton(isPlaying)).AddTo(this);
            
            worldObjectPositionAnchor.SetAnchorObject(anchorObject);
        }

        public override void ActivePauseButton(bool isActive)
        {
            playButton.gameObject.SetActive(!isActive);
            pauseButton.gameObject.SetActive(isActive);
        }

        public override void UpdateMusicSequence(float value)
        {
            sequenceSlider.value = value;
            isSliderChangedByMethod = true;
        }

        public override void SetMusicInfo(string title, string singer, string lyricsWriter, string composer)
        {
            string infoText = String.Empty;
            if (!string.IsNullOrEmpty(title))
            {
                infoText = String.Join("", infoText, $"Title:{title}");
            }

            if (!string.IsNullOrEmpty(singer))
            {
                infoText = String.Join(" ", infoText, $"/Sing by {singer}");
            }

            if (!string.IsNullOrEmpty(lyricsWriter))
            {
                infoText = String.Join(" ", infoText, $"/Lyrics by {lyricsWriter}");
            }

            if (!string.IsNullOrEmpty(composer))
            {
                infoText = String.Join(" ", infoText, $"/Composed by {composer}");
            }

            musicInfoText.text = string.IsNullOrEmpty(infoText) ? "none" : infoText;
        }

        private void Awake()
        {
            sequenceSlider.onValueChanged.AddListener(value =>
            {
                if (!isSliderChangedByMethod)
                {
                    sequenceSliderSubject.OnNext(value);
                }

                isSliderChangedByMethod = false;
            });
        }
    }
}

