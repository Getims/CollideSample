using System.Collections.Generic;
using LabraxEditor;
using LabraxStudio.Meta.GameField;
using LabraxStudio.Meta.Levels;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta
{
    public class GameSettings : ScriptableSettings
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [TitleGroup("Launch Settings")]
        [BoxGroup("Launch Settings/In", showLabel: false), SerializeField]
        private LaunchSettings _launchSettings;

        [TitleGroup("Balance Settings")]
        [BoxGroup("Balance Settings/In", showLabel: false), SerializeField]
        private BalanceSettings _balanceSettings;

        [FoldoutGroup(LEVELS_SETTINGS), SerializeField]
        private List<GameTheme> _gameThemes;
       
        [FoldoutGroup (LEVELS_SETTINGS), SerializeField]
        private GameFieldSettings _gameFieldSettings;
        
        [FoldoutGroup (LEVELS_SETTINGS), SerializeField]
        private LevelsListMeta _levelsList;
        
        [FoldoutGroup (LEVELS_SETTINGS), SerializeField]
        private List<LevelsListMeta> _selectableLevelsLists;

        [TitleGroup("Swipe settings")]
        [SerializeField]
        private SwipeSettings _swipeSettings;

        [TitleGroup("Camera settings")]
        [SerializeField]
        private CameraSettings _cameraSettings;

        [TitleGroup("Localization settings")]
        [SerializeField]
        private List<LocalizationSettingsMeta> _localizationSettings;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public LaunchSettings LaunchSettings => _launchSettings;
        public BalanceSettings BalanceSettings => _balanceSettings;
        public List<GameTheme> GameThemes => _gameThemes;
        public GameFieldSettings GameFieldSettings => _gameFieldSettings;
        public LevelsListMeta LevelsList => _levelsList;
        public List<LevelsListMeta> SelectableLevelsLists => _selectableLevelsLists;
        public SwipeSettings SwipeSettings => _swipeSettings;
        public CameraSettings CameraSettings => _cameraSettings;
        public List<LocalizationSettingsMeta> LocalizationSettings => _localizationSettings;


        private const string LEVELS_SETTINGS = "Game level settings";
    }
}