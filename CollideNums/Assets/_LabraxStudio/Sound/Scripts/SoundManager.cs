using System.Collections;
using System.Collections.Generic;
using LabraxStudio.App.Services;
using LabraxStudio.Base;
using LabraxStudio.Managers;
using LabraxStudio.Meta;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Sound
{
    public class SoundManager : SharedManager<SoundManager>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title("References")]
        [SerializeField]
        private AudioSource _gameplayAS;

        [SerializeField]
        private AudioSource _musicAS;

        [SerializeField]
        private AllGlobalSettings _allGlobalSettings;

        // PROPERTIES: ----------------------------------------------------------------------------

        private SoundSettings SoundMeta => _allGlobalSettings.SoundSettings;
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
            _isSoundOn = PlayerDataService.IsSoundOn;
            _isMusicOn = PlayerDataService.IsMusicOn;
        }
        
        public float GetCustomGameplaySoundVolume(GameplaySound meta) =>
            meta == null
                ? SoundMeta.GameplaySoundsVolume * _soundK
                : SoundMeta.GameplaySoundsVolume * _soundK * meta.SoundPercent;

        public float GetCustomGameplaySoundVolume(SFXMeta meta) =>
            meta == null
                ? SoundMeta.GameplaySoundsVolume * _soundK
                : SoundMeta.GameplaySoundsVolume * _soundK * meta.SoundPercent;

        public float GetCustomMusicVolume(BackgroundMusic meta) =>
            meta == null ? 1 : meta.SoundPrecent;

        public void SwitchSound()
        {
            _isSoundOn = !_isSoundOn;
            if(_isSoundOn)
                ResetMusicFast();
            PlayerDataService.SetSoundState(_isSoundOn);
        }

        public void SwitchMusic()
        {
            _isMusicOn = !_isMusicOn;
            if(_isMusicOn)
                ResetMusicFast();
            
            if (_isMusicOn &&  _musicAS.enabled)
                _musicAS.Play();
            else
                _musicAS.Stop();
            
            PlayerDataService.SetMusicState(_isMusicOn);
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
            
            if(_gameplayAS.enabled == false)
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

        public void PlaySoundNoRandomPitch(GameplaySounds sound)
        {
            if (!_isSoundOn)
                return;
             
            if(_gameplayAS.enabled == false)
                return;

            _canPlay = true;

            var meta = SoundMeta.GetGameplaySound(sound);
            _gameplayAS.PlayOneShot(meta.Clip, GetCustomGameplaySoundVolume(meta));
        }

        public void PlaySound(GameplaySounds sound, AudioSource source, bool oneShot = true)
        {
            if (!_isSoundOn || source == null)
                return;
             
            if(source.enabled == false)
                return;

            var meta = SoundMeta.GetGameplaySound(sound);
            source.pitch = Random.Range(SoundMeta.GameplayMinPitch, SoundMeta.GameplayMaxPitch);

            if (!oneShot)
            {
                source.clip = meta.Clip;
                source.volume = GetCustomGameplaySoundVolume(meta);
                source.Play();
            }
            else
            {
                source.PlayOneShot(meta.Clip, GetCustomGameplaySoundVolume(meta));
            }
        }

        public void PlaySound(AudioClip clip, GameplaySounds sound)
        {
            if (!_isSoundOn)
                return;
             
            if(_gameplayAS.enabled == false)
                return;

            _gameplayAS.pitch = Random.Range(SoundMeta.GameplayMinPitch, SoundMeta.GameplayMaxPitch);
            //gameplayAS.volume = GetCustomGameplaySoundVolume(_sound);
            _gameplayAS.PlayOneShot(clip, GetCustomGameplaySoundVolume(SoundMeta.GetGameplaySound(sound)));
        }

        public void PlaySound(BackgroundMusics sound, bool highPassEffect = false, int frequency = 1000)
        {
            _musicAS.pitch = 1;
            EnableBackgroundHighPass(highPassEffect, frequency);

            if (_musicCoroutine != null)
                StopCoroutine(_musicCoroutine);

            var meta = SoundMeta.GetBackgroundMusic(sound);
            if (meta == null)
            {
                Debug.LogWarning("No music meta");
                return;
            }

            _musicCoroutine = StartCoroutine(BackGroundMusicFading(meta.Clip, GetCustomMusicVolume(meta)));
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
            
            if(_isMusicOn && _musicAS.enabled)
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