using System;
using UnityEngine;

namespace LabraxStudio.Data
{
    [Serializable]
    public class LevelData
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private string _levelName;
        
        [SerializeField]
        private bool _isUnlocked = false;

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public LevelData(string levelName)
        {
            _levelName = levelName;
        }

        // PROPERTIES: ----------------------------------------------------------------------------

        public string LevelName => _levelName;

        private string ElementName =>
            $"'Name: {_levelName}'";

        public bool IsUnlocked => _isUnlocked;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetUnlocked(bool isUnlocked) => _isUnlocked = isUnlocked;
    }
}