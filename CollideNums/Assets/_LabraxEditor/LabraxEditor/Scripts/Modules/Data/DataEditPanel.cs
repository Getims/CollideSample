using UnityEditor;
using System;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
namespace LabraxEditor.Data
{
    [HideLabel]
    [Serializable]
    public class DataEditPanel
    {
        private const string LEVEL_BUTTONS_GROUP = "Level Button";
        private const string TUTORIAL_BUTTONS_GROUP = "Tutorial Button";

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void LoadData() =>
            gameData = DataManager.LoadGameData();

        public void SaveData() =>
            DataManager.SaveGameData(gameData);

        public void ClearData()
        {
            PlayerPrefs.DeleteAll();
            FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
            LoadData();
        }

        [TitleGroup("Game Data")]
        [BoxGroup("Game Data/In", showLabel: false)]
        [LabelText("Data")]
        public GameData gameData = new GameData();

        private void OnEnable()
        {
            gameData = new GameData();
            LoadData();
        }
        
    }
}
#endif