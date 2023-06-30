using System.Collections.Generic;
using LabraxStudio.Meta;
using LabraxStudio.Sound;
using UnityEngine;

namespace LabraxStudio.App.Services
{
    public class MusicService
    {
        // FIELDS: -------------------------------------------------------------------

        private bool _isInitialized;
        private SFXMeta _mainMenuBackgroundMusic;
        private SFXMeta _winScreenBackgroundMusic;
        private List<SFXMeta> _gameBackgroundMusic = new List<SFXMeta>();
        private int _currentGameMusic = 0;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;

            _mainMenuBackgroundMusic = ServicesProvider.GameSettingsService.GetSoundSettings.MainMenuBackgroundMusic;
            _winScreenBackgroundMusic = ServicesProvider.GameSettingsService.GetSoundSettings.WinScreenBackgroundMusic;
            _gameBackgroundMusic =
                new List<SFXMeta>(ServicesProvider.GameSettingsService.GetSoundSettings.GameBackgroundMusic);
            Utils.Shuffle(_gameBackgroundMusic);

            int gameMusicCount = _gameBackgroundMusic.Count;
            _currentGameMusic = Random.Range(0, gameMusicCount);
        }

        public void PlayMainMenuMusic()
        {
            if (_mainMenuBackgroundMusic == null)
            {
                Debug.LogWarning("No main menu background music!");
                return;
            }

            PlayMusic(_mainMenuBackgroundMusic);
        }

        public void PlayWinScreenMusic()
        {
            if (_winScreenBackgroundMusic == null)
            {
                Debug.LogWarning("No win screen background music!");
                return;
            }

            PlayMusic(_winScreenBackgroundMusic);
        }

        public void PlayGameMusic()
        {
            if (_gameBackgroundMusic.Count == 0)
            {
                Debug.LogWarning("No game background music!");
                return;
            }

            if (_currentGameMusic >= _gameBackgroundMusic.Count)
                _currentGameMusic = 0;

            PlayMusic(_gameBackgroundMusic[_currentGameMusic]);
            _currentGameMusic++;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void PlayMusic(SFXMeta musicMeta) => SoundManager.Instance.PlayMusic(musicMeta);
    }
}