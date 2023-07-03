using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Data
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

        [BoxGroup(Analytics), SerializeField]
        private AnalyticsData _analyticsData = new AnalyticsData();

        [BoxGroup(Analytics), SerializeField]
        private RemoteData _remoteData = new RemoteData();

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

        public AnalyticsData AnalyticsData
        {
            get => _analyticsData;
            set => _analyticsData = value;
        }

        public RemoteData RemoteData => _remoteData;

        // FIELDS: --------------------------------------------------------------------------------

        private const string MainSettings = "Main Settings";
        private const string Player = "Player Data";
        private const string Level = "Level Data";
        private const string Analytics = "Analytics Data";
    }
}