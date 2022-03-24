using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicPlayer
{
    [CreateAssetMenu(menuName = "MusicPlayer/Create MusicList")]
    public class MusicListScriptableObject : ScriptableObject
    {
        public List<MusicEntity> Musics;
    }
}