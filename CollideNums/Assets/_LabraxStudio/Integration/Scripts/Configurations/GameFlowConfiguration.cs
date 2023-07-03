using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.AnalyticsIntegration.RemoteControl
{
    [Serializable]
    public class GameFlowConfiguration
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, LabelWidth(200)]
        public string LevelsListName = string.Empty;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        internal static bool IsEqual(GameFlowConfiguration firstConfig, GameFlowConfiguration secondConfig)
        {
            return firstConfig.LevelsListName == secondConfig.LevelsListName;
        }
    }
}