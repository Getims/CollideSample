using LabraxEditor;
using LabraxStudio.AnalyticsIntegration.IAP;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.AnalyticsIntegration
{
    public class LabraxAnalyticsSettings : ScriptableSettings
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [TitleGroup(AnalyticsSettingsTitle)]
        [BoxGroup(AnalyticsSettingsGroup, showLabel: false)]
        [SerializeField]
        private bool _enableMixpanel = true;

        [BoxGroup(AnalyticsSettingsGroup), SerializeField]
        [ShowIf(nameof(_enableMixpanel))]
        private bool _useMixpanelInEditor = false;

        [TitleGroup(MonetizationSettingsTitle)]
        [BoxGroup(MonetizationSettingsGroup, showLabel: false)]
        [SerializeField]
        [HideInInspector]
        private bool _isIAPEnabled = true;

        [BoxGroup(MonetizationSettingsGroup), SerializeField]
        [HideInInspector]
        [InlineProperty, HideLabel]
        private IapSettings _iapSettings;

        [Space(10)]
        [BoxGroup(MonetizationSettingsGroup), SerializeField]
        [Min(0)]
        [HideInInspector]
        private int _chashPointsForRV = 0;

        [BoxGroup(MonetizationSettingsGroup), SerializeField]
        [Min(0)]
        [HideInInspector]
        private int _chashPointsForInterstitial = 0;

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool EnableMixpanel => _enableMixpanel;
        public bool UseMixpanelInEditor => _useMixpanelInEditor;
        public IapSettings IapSettings => _iapSettings;
        public int ChashPointsForRv => _chashPointsForRV;
        public bool IsIAPEnabled => _isIAPEnabled;
        public int ChashPointsForInterstitial => _chashPointsForInterstitial;

        // FIELDS: -------------------------------------------------------------------

        private const string AnalyticsSettingsTitle = "Analytics Settings";
        private const string AnalyticsSettingsGroup = "Analytics Settings/In";
        private const string MonetizationSettingsTitle = "Monetization Settings";
        private const string MonetizationSettingsGroup = "Monetization Settings/In";
    }
}