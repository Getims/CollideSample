using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.AnalyticsIntegration.IAP
{
    [System.Serializable]
    public class IapSettings
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        [ListDrawerSettings(ListElementLabelName = "ElementName")]
        private List<IapId> _iaps = new List<IapId>();

        [SerializeField]
        private int _noAdsBundle1Coins = 200;

        [SerializeField]
        private int _noAdsBundle2Coins = 900;

        // PROPERTIES: ----------------------------------------------------------------------------

        public List<IapId> Iaps => _iaps;
        public int NoAdsBundle1Coins => _noAdsBundle1Coins;
        public int NoAdsBundle2Coins => _noAdsBundle2Coins;
    }
}