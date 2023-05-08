using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Data
{
    [Serializable]
    public class PlayerData
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Min(0)]
        private int _money;
        
        [SerializeField, Min(0)]
        private int _currentLevel;
        
        [SerializeField, LabelText("Tutorial Done?")]
        private bool _isTutorialDone;

        [SerializeField]
        private bool _isSoundOn = true;

        [SerializeField]
        private bool _isMusicOn = true;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public int Money => _money;
        public int CurrentLevel => _currentLevel;
        public bool IsTutorialDone => _isTutorialDone;
        public bool IsSoundOn => _isSoundOn;
        public bool IsMusicOn => _isMusicOn;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetMoney(int money) =>
            _money = Mathf.Max(money, 0);

        public void SetCurrentLevel(int currentLevel) =>
            _currentLevel = Mathf.Max(currentLevel, 0);

        public void SetTutorialState(bool isTutorialDone) =>
            _isTutorialDone = isTutorialDone;

        public void SetSoundState(bool enabled) =>
            _isSoundOn = enabled;

        public void SetMusicState(bool enabled) =>
            _isMusicOn = enabled;

    }
}