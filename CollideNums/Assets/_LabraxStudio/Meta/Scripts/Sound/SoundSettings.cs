using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using LabraxEditor;
using LabraxStudio.Base;

namespace LabraxStudio.Meta
{
    [CreateAssetMenu(fileName = "Sound Settings", menuName = "🕹 Labrax Studio/Settings/Sound Settings")]
    public class SoundSettings : ScriptableSettings
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title("Settings")]
        [SerializeField]
        private float _musicFadeTime = 0.5f;

        [SerializeField]
        [Range(-3, 3)]
        private float _gameplayMinPitch = 0.9f;

        [SerializeField]
        [Range(-3, 3)]
        private float _gameplayMaxPitch = 1f;

        [SerializeField]
        [Range(0, 1)]
        private float _gameplaySoundsVolume = 1f;

        [SerializeField]
        [Range(0, 1)]
        private float _backgroundMusicVolume = 0.7f;

        [Space(10)]
        [SerializeField]
        private SFXMeta _mainMenuBackgroundMusic;

        [SerializeField]
        private List<SFXMeta> _gameBackgroundMusic = new List<SFXMeta>();

        // PROPERTIES: ----------------------------------------------------------------------------

        public float MusicFadeTime => _musicFadeTime;
        public float GameplayMinPitch => _gameplayMinPitch;
        public float GameplayMaxPitch => _gameplayMaxPitch;
        public float GameplaySoundsVolume => _gameplaySoundsVolume;
        public float BackgroundMusicVolume => _backgroundMusicVolume;
        public SFXMeta MainMenuBackgroundMusic => _mainMenuBackgroundMusic;
        public List<SFXMeta> GameBackgroundMusic => _gameBackgroundMusic;
    }
}