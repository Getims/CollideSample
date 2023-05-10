using System.Collections.Generic;
using LabraxEditor;
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
        private GameFieldSettings _gameGameFieldSettings;
        
        [BoxGroup("Game level settings/In", showLabel: false), SerializeField]
        private List<LevelMeta> _levelsList = new List<LevelMeta>();

        // PROPERTIES: ----------------------------------------------------------------------------

        public LaunchSettings LaunchSettings => _launchSettings;
        public BalanceSettings BalanceSettings => _balanceSettings;
        public GameFieldSettings GameFieldSettings => _gameGameFieldSettings;
        public List<LevelMeta> LevelsList => _levelsList;
    }
}