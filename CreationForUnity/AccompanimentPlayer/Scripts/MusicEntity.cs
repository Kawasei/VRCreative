using System;
using UnityEngine;

namespace MusicPlayer
{
    [Serializable]
    public class MusicEntity
    {
        public string Title;
        public string Singer;
        public string Composer;
        public string LyricsWriter;
        public AudioClip Music;
        public TextAsset Lyrics;
    }
}

