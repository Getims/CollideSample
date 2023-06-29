using System.Collections.Generic;
using LabraxEditor;
using LabraxStudio.Meta.Tutorial;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta.Levels
{
    public class LevelsListMeta : ScriptableMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private TutorialSettingsMeta _tutorialSettingsMeta;
        
        [SerializeField]
        [ListDrawerSettings(ListElementLabelName = "@ElementName", NumberOfItemsPerPage = 10), LabelWidth(50),
         OnValueChanged(nameof(CalculateIndexes)), OnInspectorInit(nameof(CalculateIndexes))]
        private List<LevelMeta> _levelsMetaList = new List<LevelMeta>();

        // PROPERTIES: ----------------------------------------------------------------------------

        public List<LevelMeta> List => _levelsMetaList;
        public TutorialSettingsMeta TutorialSettingsMeta => _tutorialSettingsMeta;

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