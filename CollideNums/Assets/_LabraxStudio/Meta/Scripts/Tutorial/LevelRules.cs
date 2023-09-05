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
        private bool _useSpriteTitle = true;
        
        [SerializeField]
        [ShowIf(nameof(_useSpriteTitle))]
        private Sprite _tutorialTitleSprite;
        
        [SerializeField]
        [HideIf(nameof(_useSpriteTitle))]
        private string _tutorialTitleString;

        [SerializeField]
        private bool _hideTaskPanel = false;

        [Space(5)]
        [SerializeField, ListDrawerSettings(ListElementLabelName = "ElementName", ShowIndexLabels = true)]
        private Rule[] _rules;

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool UseSpriteTitle => _useSpriteTitle;
        public Sprite TutorialTitleSprite => _tutorialTitleSprite;
        public string TutorialTitleString => _tutorialTitleString;
        public int LevelIndex => _levelIndex - 1;
        public int RulesCount => _rules.Length;
        public bool HideTaskPanel => _hideTaskPanel;

        private string LabelText => $"'Level index: {_levelIndex}'";


        // PUBLIC METHODS: ------------------------------------------------------------------------

        public Rule GetRule(int ruleIndex) => _rules[ruleIndex];
    }
}