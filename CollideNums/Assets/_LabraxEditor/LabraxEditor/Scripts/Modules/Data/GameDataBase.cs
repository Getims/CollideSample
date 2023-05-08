using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxEditor.Data
{
    [Serializable]
    public class GameDataBase
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [BoxGroup(MainSettings, showLabel: false), SerializeField]
        private string _appVersion = "0.0";
        
        [BoxGroup(MainSettings), SerializeField]
        private bool _isFirstStart = true;
        
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public string AppVersion { get => _appVersion; set => _appVersion = value; }
        public bool IsFirstStart { get => _isFirstStart; set => _isFirstStart = value; }

        // FIELDS: --------------------------------------------------------------------------------
        
        private const string MainSettings = "Main Settings";
        private const string OtherData = "Other Data";
        private const string Player = OtherData + "/Player Data";
        private const string Level = OtherData + "/Level Data";
    }
}
