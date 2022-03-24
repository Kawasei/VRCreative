using MusicPlayer.Setting;
using UniRx;
using UnityEngine;

namespace MusicPlayer.Core.AudioSystem
{
    public static class AudioSystemFactory
    {
        public static IAudioSystem CreateAudioSystem(MonoBehaviour behaviour, AudioSystemSetting setting)
        {
            IAudioSystem audioSystem = null;

            switch (setting.AudioSystemType)
            {
                case AudioSystemTypes.Unity:
                    audioSystem = new UnityAudioSystem();
                    break;
                default:
                    Debug.LogError($"Invalid Audio System Type:{setting.AudioSystemType}");
                    break;
            }

            audioSystem.Setup(behaviour, setting);
            return audioSystem;
        }
    }
}
