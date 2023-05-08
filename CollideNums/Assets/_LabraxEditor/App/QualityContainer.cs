using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabraxStudio.Base;

namespace LabraxStudio.App
{
    public enum QualitySettingsName
    {
        Menu,
        Game
    }

    [System.Serializable]
    public class QualityContainer
    {
        [SerializeField] private QualitySettingsName _settingsName;
        [SerializeField] private string _presetName;

        public QualitySettingsName SettingsName => _settingsName;
        public string PresetName => _presetName;
    }
}
