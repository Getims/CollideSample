using System;
using System.Globalization;
using LabraxStudio.App;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxEditor.Data
{
    [Serializable]
    public class DataManager
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [BoxGroup("Game Data", showLabel: false)]
        [SerializeField]
        private GameData _gameData;

        // PROPERTIES: ----------------------------------------------------------------------------

        public GameData GameData => _gameData;

        // FIELDS: --------------------------------------------------------------------------------

        private const string PrefKey = "GAME_DATA";
        public static int SessionStartTime;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        [Button(ButtonHeight = 35)]
        public void PrepareData()
        {
            LoadData();

            string currentVersion = AppDirector.GetApplicationVersion();
            float version = 0;

            if (_gameData != null)
            {
                try
                {
                    version = float.Parse(_gameData.AppVersion, CultureInfo.InvariantCulture.NumberFormat);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }

                /*
                if (version < 0.7f)
                    _gameData = null;
                    */
            }

            if (_gameData == null)
            {
                CreateData();
                CheckDataFields();
                SaveGameData();
            }
            else
            {
                CheckDataFields();
            }

            if (_gameData == null)
                return;

            if (!currentVersion.Equals(_gameData.AppVersion))
                _gameData.AppVersion = currentVersion;
        }

        [Button(ButtonHeight = 35)]
        public void SaveGameData() =>
            SaveData(_gameData);

        public void ClearGameData()
        {
            CreateData();
            CheckDataFields();
            SaveGameData();
        }

        public static void SaveGameData(GameData gameData) =>
            SaveData(gameData);

        public static GameData LoadGameData()
        {
            string dataStr = SecurePreferences.GetString(PrefKey);
            GameData gameData = JsonUtility.FromJson<GameData>(dataStr);

            if (gameData == null)
            {
                gameData = new GameData
                {
                    AppVersion = AppDirector.GetApplicationVersion()
                };

                CheckDataFields(gameData);

                dataStr = JsonUtility.ToJson(gameData);
                SecurePreferences.SetString(PrefKey, dataStr);
                SecurePreferences.SaveData();
            }
            else
            {
                CheckDataFields(gameData);
            }

            return gameData;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CreateData()
        {
            _gameData = new GameData();
            _gameData.AppVersion = AppDirector.GetApplicationVersion();
        }

        private void LoadData()
        {
            string dataStr = SecurePreferences.GetString(PrefKey);
            _gameData = JsonUtility.FromJson<GameData>(dataStr);
        }

        private static void SaveData(GameData gameData)
        {
            string dataStr = JsonUtility.ToJson(gameData);
            SecurePreferences.SetString(PrefKey, dataStr);
            SecurePreferences.SaveData();
        }

        private void CheckDataFields() =>
            CheckDataFields(_gameData);

        private static void CheckDataFields(GameData gameData)
        {
            /*
            gameData.PlayerData ??= new();
            gameData.LevelsData ??= new();
            */
        }
    }
}