#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace LabraxEditor
{
    public class EditorSettingsWindow : OdinEditorWindow
    {
        // FIELDS: -------------------------------------------------------------------

        private static EditorSettings _settings = null;
        private SettingsEditPanel _settingsPanel = new SettingsEditPanel();

        [SerializeField]
        [ListDrawerSettings(HideAddButton = true, DraggableItems = false, Expanded = true)]
        private List<MetaTabSettings> _tabs = null;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        protected override void OnGUI()
        {
            if (_settings == null)
                Close();

            if (_tabs == null)
            {
                if (_settings.MenuType == MenuType.Meta)
                    _tabs = new List<MetaTabSettings>(_settings.MetasTabs);
                else if (_settings.MenuType == MenuType.Settings)
                    _tabs = new List<MetaTabSettings>(_settings.SettingsTabs);
            }

            base.OnGUI();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------
        public static void OpenWindow(EditorSettings settings)
        {
            /*
            _settings = settings;
            var window = GetWindow<EditorSettingsWindow>();
            window.ShowPopup();*/

            _settings = settings;
            EditorSettingsWindow window = CreateInstance<EditorSettingsWindow>();
            float width = 300;
            float height = 600;

            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(width, height);

            window.ShowModalUtility();
        }
    }
}
#endif