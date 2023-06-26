﻿using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Data
{
    [Serializable]
    public class LevelsData
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private string _levelsListName = string.Empty;
        
        [SerializeField]
        [ListDrawerSettings(ListElementLabelName = "ElementName", ShowIndexLabels = true)]
        private List<LevelData> _levelDatas = new List<LevelData>();

        // PROPERTIES: ----------------------------------------------------------------------------

        public List<LevelData> LevelDatas => _levelDatas;
        public string LevelsListName => _levelsListName;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public LevelData GetLevelData(string levelName) =>
            _levelDatas.Find(d => d.LevelName == levelName);

        public void AddLevelData(LevelData levelData) =>
            _levelDatas.Add(levelData);

        public void SetLevelsListName(string listName)
        {
            _levelsListName = listName;
        }
    }
}