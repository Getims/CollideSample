using System.Collections;
using System.Collections.Generic;
using LabraxStudio.App.Services;
using LabraxStudio.Managers;
using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Sound
{
    public class SoundManager : SharedManager<SoundManager>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private AudioSource _gameplayAS;

        [SerializeField]
        private AudioSource _musicAS;

        // PROPERTIES: ----------------------------------------------------------------------------

        private SoundSettings SoundMeta => ServicesProvider.GameSettingsService.GetSoundSettings;
        public bool IsSoundOn => _isSoundOn;
        public bool IsMusicOn => _isMusicOn;

        // FIELDS: --------------------------------------------------------------------------------

        private readonly List<GSound> _interfaceSoundList = new List<GSound>();
        private bool _isSoundOn = true;
        private bool _isMusicOn = true;

        private Coroutine _musicCoroutine;
        private float _lastBgPercent = 0;
        private long _timeNow;
        private int _musicK = 1;
        private int _soundK = 1;
        private bool _canPlay = true;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Setup()
        {
            _interfaceSoundList.Clear();
            _isSoundOn = ServicesProvider.PlayerDataService.IsSoundOn;
            _isMusicOn = ServicesProvider.PlayerDataService.IsMusicOn;
        }

        public float GetCustomGameplaySoundVolume(SFXMeta meta) =>
            meta == null
                ? SoundMeta.GameplaySoundsVolume * _soundK
                : SoundMeta.GameplaySoundsVolume * _soundK * meta.SoundPercent;

        public void SwitchSound()
        {
            _isSoundOn = !_isSoundOn;
            if (_isSoundOn)
                ResetMusicFast();
            ServicesProvider.PlayerDataService.SetSoundState(_isSoundOn);
        }

        public void SwitchMusic()
        {
            _isMusicOn = !_isMusicOn;
            if (_isMusicOn)
                ResetMusicFast();

            if (_isMusicOn && _musicAS.enabled)
                _musicAS.Play();
            else
                _musicAS.Stop();

            ServicesProvider.PlayerDataService.SetMusicState(_isMusicOn);
        }

        public void SetAllMusicOff()
        {
#if UNITY_IPHONE && !UNITY_EDITOR
            _musicAS.enabled = false;
            _gameplayAS.enabled = false;
#endif
        }

        public void ResetMusic()
        {
#if UNITY_IPHONE && !UNITY_EDITOR
            _musicAS.enabled = false;
            _gameplayAS.enabled = false;

            AudioSettings.Reset(AudioSettings.GetConfiguration());
            Utils.PerformWithDelay(this, 1.25f, () =>
            {
                _musicAS.enabled = true;
                _gameplayAS.enabled = true;
                _musicAS.mute = false;
                _gameplayAS.mute = false;
                AudioListener.volume = 1;
                
                if (_isMusicOn)
                    _musicAS.Play();  
            });
#endif
        }

        public void ResetMusicFast()
        {
#if UNITY_IPHONE && !UNITY_EDITOR
            AudioSettings.Reset(AudioSettings.GetConfiguration());
            _musicAS.enabled = true;
            _gameplayAS.enabled = true;
            _musicAS.mute = false;
            _gameplayAS.mute = false;
            AudioListener.volume = 1;
#endif
        }

        public void PlaySound(SFXMeta SFXMeta, bool ignoreTime = true, float pauseTime = 1.0f)
        {
            if (!_isSoundOn)
                return;

            if (SFXMeta.IsDisabled)
                return;

            if (_gameplayAS.enabled == false)
                return;

            _canPlay = true;

            if (!ignoreTime)
            {
                GSound gsound = _interfaceSoundList.Find(s => s.Id == SFXMeta.FileName);
                _timeNow = UnixTime.Now;

                if (gsound == null)
                {
                    GSound sound = new GSound(SFXMeta.FileName, _timeNow);
                    _interfaceSoundList.Add(sound);
                }
                else
                {
                    _canPlay = (_timeNow - gsound.LastPlayTime) >= pauseTime;

                    if (_canPlay)
                        gsound.SetLatPlayTime(_timeNow);
                }
            }

            if (!_canPlay)
                return;

            _gameplayAS.pitch = Random.Range(SoundMeta.GameplayMinPitch, SoundMeta.GameplayMaxPitch);
            _gameplayAS.PlayOneShot(SFXMeta.AudioClip, GetCustomGameplaySoundVolume(SFXMeta));
        }

        public void PlaySoundAndCreateAudioSource(SFXMeta SFXMeta)
        {
            if (!_isSoundOn)
                return;

            if (SFXMeta.IsDisabled)
                return;

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.pitch = Random.Range(0.95f, 1.01f);
            audioSource.priority = 100;
            audioSource.PlayOneShot(SFXMeta.AudioClip, GetCustomGameplaySoundVolume(SFXMeta));

            Destroy(audioSource, 5);
        }

        public void PlayMusic(SFXMeta SFXMeta, bool highPassEffect = false, int frequency = 1000)
        {
            if (SFXMeta == null)
            {
                Debug.LogWarning("No music meta");
                return;
            }

            if (SFXMeta.IsDisabled)
                return;

            _musicAS.pitch = 1;
            EnableBackgroundHighPass(highPassEffect, frequency);

            if (_musicCoroutine != null)
                StopCoroutine(_musicCoroutine);

            _musicCoroutine = StartCoroutine(BackGroundMusicFading(SFXMeta.AudioClip, SFXMeta.SoundPercent));
        }

        public void StopBGMusic()
        {
            _musicAS.Stop();
        }

        public void SetBGMusicPitch(float pitch)
        {
            if (pitch < 1)
                pitch = 1;
            _musicAS.pitch = pitch;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void EnableBackgroundHighPass(bool state = false, int frequency = 1000)
        {
            AudioHighPassFilter audioHighPassFilter = _musicAS.GetComponent<AudioHighPassFilter>();
            audioHighPassFilter.cutoffFrequency = frequency;
            audioHighPassFilter.enabled = state;
        }

        private IEnumerator BackGroundMusicFading(AudioClip clip, float newPercent)
        {
            _musicAS.volume = SoundMeta.BackgroundMusicVolume * _musicK * _lastBgPercent;
            float elapsedTime = 0;

            while (elapsedTime <= SoundMeta.MusicFadeTime)
            {
                _musicAS.volume = Mathf.Lerp(SoundMeta.BackgroundMusicVolume * _musicK * _lastBgPercent, 0,
                    elapsedTime / SoundMeta.MusicFadeTime);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            elapsedTime = 0;
            _musicAS.volume = 0;
            _musicAS.Stop();
            _musicAS.clip = clip;

            if (_isMusicOn && _musicAS.enabled)
                _musicAS.Play();

            _lastBgPercent = newPercent;
            while (elapsedTime <= SoundMeta.MusicFadeTime)
            {
                _musicAS.volume = Mathf.Lerp(0, SoundMeta.BackgroundMusicVolume * _musicK * _lastBgPercent,
                    elapsedTime / SoundMeta.MusicFadeTime);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            _musicAS.volume = SoundMeta.BackgroundMusicVolume * _musicK * _lastBgPercent;
        }
    }
}