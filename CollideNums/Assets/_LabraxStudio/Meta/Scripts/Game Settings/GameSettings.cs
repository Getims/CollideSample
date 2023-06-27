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

        [TitleGroup("Game level settings")]
        [BoxGroup("Game level settings/In", showLabel: false), SerializeField]
        private GameFieldSprites _gameFieldSprites;
        
        [BoxGroup("Game level settings/In", showLabel: false), SerializeField]
        private GameFieldSettings _gameFieldSettings;
        
        [BoxGroup("Game level settings/In", showLabel: false), SerializeField]
        private LevelsListMeta _levelsList;
        
        [BoxGroup("Game level settings/In"), SerializeField]
        private List<LevelsListMeta> _selectableLevelsLists;

        // PROPERTIES: ----------------------------------------------------------------------------

        public LaunchSettings LaunchSettings => _launchSettings;
        public BalanceSettings BalanceSettings => _balanceSettings;
        public GameFieldSprites GameFieldSprites => _gameFieldSprites;
        public GameFieldSettings GameFieldSettings => _gameFieldSettings;
        public LevelsListMeta LevelsList => _levelsList;
        public List<LevelsListMeta> SelectableLevelsLists => _selectableLevelsLists;
    }
}