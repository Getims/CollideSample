using LabraxEditor;
using UnityEngine;

namespace LabraxStudio.Meta
{
    public class LevelMeta : ScriptableMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private string _levelName = "Level";

        // FIELDS: --------------------------------------------------------------------------------

        private int _levelNumber;

        // PROPERTIES: ----------------------------------------------------------------------------

        public string LevelName => _levelName;
        private string ElementName => $"{_levelNumber}";

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetLevelNumber(int value) =>
            _levelNumber = value;
    }
}