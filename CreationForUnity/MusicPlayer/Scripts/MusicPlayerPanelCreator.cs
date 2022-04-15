using MusicPlayer.ControlPanel.View;
using MusicPlayer.Core.Controller;
using UniRx;
using MusicPlayer.WorldObject;
using UnityEngine;

namespace MusicPlayer
{
    public class MusicPlayerPanelCreator : MonoBehaviour
    {
        [SerializeField] private MusicPlayerWorldObject musicPlayerWorldObject;
        
        [SerializeField] private MusicPlayerController controller;
        //[SerializeField] private ControlPanelSettingScriptableObject controlPanelSetting;
        
        [SerializeField] private AbstractMusicPlayerControlPanelView controlPanelView;

        private void Awake()
        {
            musicPlayerWorldObject.OnInteractive.Subscribe(_ => createControlPanel());
        }

        private void createControlPanel()
        {
            //TODO 雑だけど一旦UIでしか作ってないので型を見る
            if (controlPanelView is UIMusicPlayerControlPanelView)
            {
                var newControlPanelView = Instantiate<AbstractMusicPlayerControlPanelView>(controlPanelView);
                newControlPanelView.Setup(controller,this.gameObject);
            }
        }
    }
}

