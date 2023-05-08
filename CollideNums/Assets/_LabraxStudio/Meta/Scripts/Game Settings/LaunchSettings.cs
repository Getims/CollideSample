using System;
using UnityEngine;

namespace LabraxStudio.Meta
{
    [Serializable]
    public class LaunchSettings
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private bool _enableDebug;

        [SerializeField]
        private bool _enableTutorial = true;

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool EnableTutorial => _enableTutorial;
    }
}