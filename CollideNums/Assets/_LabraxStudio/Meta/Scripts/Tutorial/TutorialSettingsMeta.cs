using System.Collections.Generic;
using LabraxEditor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta.Tutorial
{
    [CreateAssetMenu(fileName = "Tutorial Settings", menuName = "🕹 Labrax Studio/Settings/Tutorial Settings")]
    public class TutorialSettingsMeta : ScriptableObject
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, ListDrawerSettings(ListElementLabelName = "LabelText")]
        private List<LevelRules> _levelsRules = new List<LevelRules>();

        // PROPERTIES: ----------------------------------------------------------------------------

        public List<LevelRules> LevelsRules => _levelsRules;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public LevelRules GetRules(int levelIndex) => _levelsRules.Find(lr => lr.LevelIndex == levelIndex);
    }
}