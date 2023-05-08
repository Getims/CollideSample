using System;
using LabraxStudio.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxEditor.Data
{
    [Serializable]
    public class GameData
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [BoxGroup(MainSettings, showLabel: false), SerializeField]
        private string _appVersion = "0.0";

        [BoxGroup(MainSettings), SerializeField]
        private bool _isFirstStart = true;

        [BoxGroup(Player), SerializeField]
        private PlayerData _playerData = new PlayerData();

        [BoxGroup(Level), SerializeField]
        private LevelsData _levelsData = new LevelsData();

        // PROPERTIES: ----------------------------------------------------------------------------

        public string AppVersion
        {
            get => _appVersion;
            set => _appVersion = value;
        }

        public bool IsFirstStart
        {
            get => _isFirstStart;
            set => _isFirstStart = value;
        }

        public PlayerData PlayerData
        {
            get => _playerData;
            set => _playerData = value;
        }

        public LevelsData LevelsData
        {
            get => _levelsData;
            set => _levelsData = value;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private const string MainSettings = "Main Settings";
        private const string Player = "Player Data";
        private const string Level = "Level Data";
    }
}