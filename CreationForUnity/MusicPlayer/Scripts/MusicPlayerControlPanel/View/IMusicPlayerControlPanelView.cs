using System;
using MusicPlayer.Core.Controller;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MusicPlayer.ControlPanel.View
{
    public interface IMusicPlayerControlPanelView
    {
        void Setup(MusicPlayerController controller, GameObject anchorObject);
        IObservable<Unit> OnClickPlayButton { get; }
        IObservable<Unit> OnClickPauseButton { get; }
        IObservable<Unit> OnClickStopButton { get; }
        IObservable<Unit> OnClickFastForwardButton { get; }
        IObservable<Unit> OnClickRewindButton { get; }
        IObservable<float> OnChangedMusicSequenceByUser { get; }
        IObservable<Unit> OnClickCloseButton { get; }
        IObservable<Unit> OnClickOpenSelectorButton { get; }
        IObservable<Unit> OnClickLyricButton { get; }
        IObservable<Unit> OnClickSettingButton { get; }
        void SetMusicInfo(string title, string singer, string lyricsWriter, string composer);
        void UpdateMusicSequence(float value);
        void ActivePauseButton(bool isActive);
    }
}
