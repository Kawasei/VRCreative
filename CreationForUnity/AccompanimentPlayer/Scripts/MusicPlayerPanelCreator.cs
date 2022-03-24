using System;
using System.Collections;
using System.Collections.Generic;
using MusicPlayer.ControlPanel.View;
using MusicPlayer.Core.Controller;
using MusicPlayer.Setting;
using UnityEngine;

namespace MusicPlayer
{
    public class MusicPlayerPanelCreator : MonoBehaviour
    {
        [SerializeField] private MusicPlayerController controller;
        //[SerializeField] private ControlPanelSettingScriptableObject controlPanelSetting;
        
        [SerializeField] private AbstractMusicPlayerControlPanelView controlPanelView;

        #if UNITY_EDITOR
        private void Start()
        {
            CreateControlPanel();
        }
        #endif

        public void CreateControlPanel()
        {
            //TODO 雑だけど一旦UIでしか作ってないので型を見る
            if (controlPanelView is UIMusicPlayerControlPanelView)
            {
                var newControlPanelView = Instantiate<AbstractMusicPlayerControlPanelView>(controlPanelView);
                newControlPanelView.Setup(controller);
            }
        }
    }
}

