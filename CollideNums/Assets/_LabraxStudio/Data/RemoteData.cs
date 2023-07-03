using System;
using LabraxStudio.AnalyticsIntegration;
using LabraxStudio.AnalyticsIntegration.RemoteControl;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Data
{
    [Serializable]
    public class RemoteData
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField, LabelWidth(150f)]
        private bool _wasSet = false;

        [SerializeField]
        private GameFlowConfiguration _gameFlowConfiguration = new GameFlowConfiguration();

        [SerializeField]
        private string _configSource = LabraxAnalyticsConstants.ConfigSourceBuild;

        [SerializeField]
        private int _abTestId = -1;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public bool WasSet
        {
            get => _wasSet;
            set => _wasSet = value;
        }

        public GameFlowConfiguration GameFlowConfiguration
        {
            get => _gameFlowConfiguration;
            set => _gameFlowConfiguration = value;
        }

        public string GameConfigSource
        {
            get => _configSource;
            set => _configSource = value;
        }

        public int AbTestId
        {
            get => _abTestId;
            set => _abTestId = value;
        }
    }
}