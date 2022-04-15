using MusicPlayer.Core.Controller;
using MusicPlayer.ControlPanel.View;
using UniRx;
using UnityEngine;

namespace MusicPlayer.Core.Presenter
{
    public class MusicPlayerPresenter : MonoBehaviour
    {
        
        [SerializeField] private AbstractMusicPlayerControlPanelView musicPlayerControlPanelView;
        [SerializeField] private MusicPlayerController controller;

        private void Awake()
        {
            musicPlayerControlPanelView.OnClickPlayButton.Subscribe(_ => controller.PlayMusic()).AddTo(this);
            musicPlayerControlPanelView.OnClickPauseButton.Subscribe(_ => controller.PauseMusic()).AddTo(this);
            musicPlayerControlPanelView.OnClickStopButton.Subscribe(_ => controller.StopMusic()).AddTo(this);
            musicPlayerControlPanelView.OnClickRewindButton.Subscribe(_ => controller.RewindMusic()).AddTo(this);
            musicPlayerControlPanelView.OnClickFastForwardButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);
            musicPlayerControlPanelView.OnChangedMusicSequenceByUser.Subscribe(value => controller.SetMusicSequence(value)).AddTo(this);
            //view.OnClickLyricButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);
            //view.OnClickOpenSelectorButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);
            //view.OnClickSettingButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);
            //view.OnClickCloseButton.Subscribe(_ => controller.FastForwardMusic()).AddTo(this);

            controller.OnMusicSequenceChanged.Subscribe(value => musicPlayerControlPanelView.UpdateMusicSequence(value)).AddTo(this);
            controller.OnPlayingMusic.Subscribe(isPlaying => musicPlayerControlPanelView.ActivePauseButton(isPlaying)).AddTo(this);
        }
    }
}
