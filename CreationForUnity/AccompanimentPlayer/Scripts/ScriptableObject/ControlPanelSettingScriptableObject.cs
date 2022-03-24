using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicPlayer.Setting
{
    [CreateAssetMenu(menuName = "MusicPlayer/Create ControlPanelSetting")]
    public class ControlPanelSettingScriptableObject : ScriptableObject
    {
        public enum PanelTypes
        {
            UI,
            Model
        }

        public enum RelativeTargetType
        {
            MainCamera,
            GameObject,
        }

        public PanelTypes Type;

        public RelativeTargetType PositionTargetType;
        public float RelativeTargetDistance;
    }
}

