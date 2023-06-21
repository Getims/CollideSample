using System.Collections.Generic;
using LabraxEditor;
using Sirenix.OdinInspector;

namespace LabraxStudio.Meta.Levels
{
    public class LevelsListMeta : ScriptableMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [ListDrawerSettings(ListElementLabelName = "@ElementName", NumberOfItemsPerPage = 10), LabelWidth(50),
         OnValueChanged(nameof(CalculateIndexes)), OnInspectorInit(nameof(CalculateIndexes))]
        private List<LevelMeta> _levelsMetaList = new List<LevelMeta>();

        // PROPERTIES: ----------------------------------------------------------------------------

        public List<LevelMeta> List => _levelsMetaList;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CalculateIndexes()
        {
#if UNITY_EDITOR
            int i = 1;

            foreach (var levelMeta in _levelsMetaList)
            {
                if (levelMeta != null)
                    levelMeta.SetLevelNumber(i);
                i++;
            }
#endif
        }
    }
}