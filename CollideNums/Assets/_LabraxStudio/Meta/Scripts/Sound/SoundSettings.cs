using System.Collections;
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

        [SerializeField, HideInInspector]
        [ListDrawerSettings(ListElementLabelName = "_soundType")]
        private List<GameplaySound> _gameplaySounds;

        [SerializeField]
        private List<BackgroundMusic> _backgroundMusic;

        public List<GameplaySound> GameplaySounds => _gameplaySounds;
        public List<BackgroundMusic> BackgroundMusics => _backgroundMusic;

        public float MusicFadeTime => _musicFadeTime;
        public float GameplayMinPitch => _gameplayMinPitch;
        public float GameplayMaxPitch => _gameplayMaxPitch;
        public float GameplaySoundsVolume => _gameplaySoundsVolume;
        public float BackgroundMusicVolume => _backgroundMusicVolume;

        public GameplaySound GetGameplaySound(GameplaySounds sound)
        {
            var sounds = _gameplaySounds.FindAll(_gs => _gs.SoundType == sound);
            if (sounds != null && sounds.Count > 0)
            {
                int i = Random.Range(0, sounds.Count);
                return sounds[i];
            }

            return null;
        }

        public BackgroundMusic GetBackgroundMusic(BackgroundMusics sound)
        {
            var sounds = _backgroundMusic.FindAll(_bm => _bm.MusicType == sound);
            if (sounds != null && sounds.Count > 0)
            {
                int i = Random.Range(0, sounds.Count);
                return sounds[i];
            }

            return null;
        }
    }

    [System.Serializable]
    public class GameplaySound
    {
        [SerializeField]
        private GameplaySounds _soundType;

        [SerializeField]
        private AudioClip _clip;

        [SerializeField]
        [Range(0f, 1f)]
        private float _soundPrecent = 1;

        public GameplaySounds SoundType => _soundType;
        public AudioClip Clip => _clip;
        public float SoundPrecent { get => _soundPrecent; }
    }

    [System.Serializable]
    public class BackgroundMusic
    {
        [SerializeField]
        private BackgroundMusics _musicType;

        [SerializeField]
        private AudioClip _clip;

        [SerializeField]
        [Range(0f, 1f)]
        private float _soundPrecent = 1;

        public BackgroundMusics MusicType => _musicType;
        public AudioClip Clip => _clip;
        public float SoundPrecent { get => _soundPrecent; }
    }
}