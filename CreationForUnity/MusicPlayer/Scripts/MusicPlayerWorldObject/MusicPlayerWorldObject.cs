using System;
using MusicPlayer.ControlPanel.View;
using MusicPlayer.Core.Controller;
using UniRx;
using UnityEngine;
using VRLibrary.Infrastructure.UnityMonoBehaviour;

namespace MusicPlayer.WorldObject
{
    public class MusicPlayerWorldObject : MonoBehaviour
    {
        [SerializeField] private InteractiveItemBehaviour interactiveItemBehaviour;
        
        [SerializeField] private MusicPlayerController controller;

        [SerializeField] private AbstractMusicPlayerControlPanelView controlPanelView;

        public IObservable<Unit> OnInteractive => interactiveItemBehaviour.UnityEvent.AsObservable();
    }
}


