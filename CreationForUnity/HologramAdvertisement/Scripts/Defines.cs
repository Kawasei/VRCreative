using System;
using UnityEngine;

namespace Creation.HologramAdvertisement
{
    [Serializable]
    public class Advertisement
    {
        [SerializeField] private Texture texture;
        [SerializeField] private AudioClip audioClip;
        
        public Texture Texture => texture;
        public AudioClip AudioClip => audioClip;
    }
    
    public enum SizeAdjustType
    {
        ShortSide,
        LongSide,
        Height,
        Width
    }
    
    public enum AdvertisementDirection
    {
        Fixed,
        Rotate,
        MainCamera,
    }

    public enum LineupDirection
    {
        Vertical,
        Horizontal,
    }

    public enum ShowAnimationType
    {
        FadeIn,
        Expansion,
        ExpansionHorizontal,
        ExpansionVertical
    }

    public enum HideAnimationType
    {
        FadeOut,
        Shrink,
        ShrinkHorizontal,
        ShrinkVertical
    }

    public enum AnchorPoint
    {
        Center,
        TopLeft,
        Top,
        TopRight,
        Left,
        Right,
        BottomLeft,
        Bottom,
        BottomRight
    }
}