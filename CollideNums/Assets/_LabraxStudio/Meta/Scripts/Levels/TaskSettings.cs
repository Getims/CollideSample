using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace LabraxStudio.Meta.Levels
{
    [Serializable]
    public class TaskSettings
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, OnValueChanged(nameof(CheckCount))]
        private List<LevelTaskMeta> _levelTasks = new List<LevelTaskMeta>();

        // PROPERTIES: ----------------------------------------------------------------------------

        public List<LevelTaskMeta> LevelTasks => _levelTasks;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CheckCount()
        {
            if (_levelTasks.Count >= 5)
                _levelTasks.SetLength(5);
        }
    }
}