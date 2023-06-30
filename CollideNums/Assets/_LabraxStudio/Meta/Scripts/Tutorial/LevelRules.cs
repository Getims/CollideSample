using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta.Tutorial
{
    [Serializable]
    public class LevelRules
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField]
        private int _levelIndex;

        [SerializeField]
        private Sprite _tutorialTitleSprite;
        
        [Space(5)]
        [SerializeField, ListDrawerSettings(ListElementLabelName = "ElementName", ShowIndexLabels = true)]
        private Rule[] _rules;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public Sprite TutorialTitleSprite => _tutorialTitleSprite;
        
        private string LabelText => $"'Level index: {_levelIndex}'";

        public int LevelIndex => _levelIndex-1;

        public int RulesCount => _rules.Length;
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public Rule GetRule(int ruleIndex) => _rules[ruleIndex];
    }
}