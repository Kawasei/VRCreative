using MusicPlayer.Core.Controller;
using MusicPlayer.ControlPanel.View;
using UniRx;
using UnityEngine;

namespace MusicPlayer.Core.Presenter
{
    public class MusicPlayerPresenter : MonoBehaviour
    {
        
        [SerializeField] private AbstractMusicPlayerControlPanelView musicPlayerView;
        [SerializeField] private MusicPlayerController controller;

        private void Awake()
        {
            musicPlayerView.OnClickPlayButton.Subscribe(_ => controller.PlayMusic()).AddTo(this);
            musicPlayerView.OnClickPauseButton.Subscribe(_ => controller.PauseMusic()).AddTo(this);
            musicPlayerView.OnClickStopButton.Subscribe(_ => controller.StopMusic()).AddTo(this);
            musicPlayerView.OnClickRewindButton.Subscribe(_ => controller.RewindMusic()).AddTo(this);
            musicPlayerView.OnClickFastForwardButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);
            musicPlayerView.OnChangedMusicSequenceByUser.Subscribe(value => controller.SetMusicSequence(value)).AddTo(this);
            //view.OnClickLyricButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);
            //view.OnClickOpenSelectorButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);
            //view.OnClickSettingButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);
            //view.OnClickCloseButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);

            controller.OnMusicSequenceChanged.Subscribe(value => musicPlayerView.UpdateMusicSequence(value)).AddTo(this);
            controller.OnPlayingMusic.Subscribe(isPlaying => musicPlayerView.ActivePauseButton(isPlaying)).AddTo(this);
        }
    }
}
