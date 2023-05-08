#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxEditor
{
    [Serializable]
    public class SettingsEditPanel
    {
        [SerializeField, InlineEditor(InlineEditorObjectFieldModes.Hidden, 
             DrawHeader = false,
             Expanded = true)]
        private EditorSettings _settings;

        public void SetSettings(EditorSettings settings)
        {
            _settings = settings;
        }
    }
}
#endif
