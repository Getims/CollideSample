using LabraxEditor;
using UnityEngine;

namespace LabraxStudio.Meta
{
    public class AllGlobalSettings : ScriptableSettings
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private bool _usesUnityLogs = true;
        
        [SerializeField]
        private GameSettings _gameSettings;

        [SerializeField]
        private SoundSettings _soundSettings;

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool UsesUnityLogs => _usesUnityLogs;
        public GameSettings GameSettings => _gameSettings;
        public SoundSettings SoundSettings => _soundSettings;
        
    }
}