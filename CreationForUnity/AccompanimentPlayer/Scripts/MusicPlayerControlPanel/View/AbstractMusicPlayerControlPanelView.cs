using System;
using MusicPlayer.Core.Controller;
using UnityEngine;
using UniRx;

namespace MusicPlayer.ControlPanel.View
{
    public abstract class AbstractMusicPlayerControlPanelView : MonoBehaviour,IMusicPlayerControlPanelView
    {
        public abstract void Setup(MusicPlayerController controller);
        public abstract IObservable<Unit> OnClickPlayButton { get; }
        public abstract IObservable<Unit> OnClickPauseButton { get; }
        public abstract IObservable<Unit> OnClickStopButton { get; }
        public abstract IObservable<Unit> OnClickFastForwardButton { get; }
        public abstract IObservable<Unit> OnClickRewindButton { get; }
        public abstract IObservable<float> OnChangedMusicSequenceByUser { get; }
        public abstract IObservable<Unit> OnClickCloseButton { get; }
        public abstract IObservable<Unit> OnClickOpenSelectorButton { get; }
        public abstract IObservable<Unit> OnClickLyricButton { get; }
        public abstract IObservable<Unit> OnClickSettingButton { get; }
        public abstract void SetMusicInfo(string title, string singer, string lyricsWriter, string composer);
        public abstract void UpdateMusicSequence(float value);
        public abstract void ActivePauseButton(bool isActive);
    }
}
