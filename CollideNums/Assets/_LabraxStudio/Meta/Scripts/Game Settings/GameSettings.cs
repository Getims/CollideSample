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

        // PROPERTIES: ----------------------------------------------------------------------------

        public LaunchSettings LaunchSettings => _launchSettings;
        public BalanceSettings BalanceSettings => _balanceSettings;

    }
}