using UnityEngine;
using LabraxStudio.Base;

namespace LabraxStudio.Meta
{
    [System.Serializable]
    public class BackgroundMusic
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private BackgroundMusics _musicType;

        [SerializeField]
        private AudioClip _clip;

        [SerializeField]
        [Range(0f, 1f)]
        private float _soundPercent = 1;

        // PROPERTIES: ----------------------------------------------------------------------------

        public BackgroundMusics MusicType => _musicType;
        public AudioClip Clip => _clip;
        public float SoundPrecent => _soundPercent;
    }
}