using UnityEngine;
using LabraxStudio.Base;

namespace LabraxStudio.Meta
{
    [System.Serializable]
    public class GameplaySound
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private GameplaySounds _soundType;

        [SerializeField]
        private AudioClip _clip;

        [SerializeField]
        [Range(0f, 1f)]
        private float _soundPercent = 1;

        // PROPERTIES: ----------------------------------------------------------------------------

        public GameplaySounds SoundType => _soundType;
        public AudioClip Clip => _clip;
        public float SoundPercent => _soundPercent;
    }
}