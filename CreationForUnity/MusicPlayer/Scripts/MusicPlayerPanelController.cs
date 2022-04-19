using MusicPlayer.ControlPanel.View;
using MusicPlayer.Core.Controller;
using UniRx;
using MusicPlayer.WorldObject;
using UnityEngine;

namespace MusicPlayer
{
    public class MusicPlayerPanelController : MonoBehaviour
    {
        [SerializeField] private MusicPlayerWorldObject musicPlayerWorldObject;
        
        [SerializeField] private MusicPlayerController controller;
        //[SerializeField] private ControlPanelSettingScriptableObject controlPanelSetting;
        
        [SerializeField] private AbstractMusicPlayerControlPanelView controlPanelView;

        private void Awake()
        {
            controlPanelView.gameObject.SetActive(false);
            
            musicPlayerWorldObject.OnInteractive.Subscribe(_ => activeControlPanel());
        }

        private void activeControlPanel()
        {
            if (controlPanelView.gameObject.activeSelf)
            {
                return;
            }
            controlPanelView.gameObject.SetActive(true);
            controlPanelView.Setup(controller,this.gameObject);
        }
    }
}

