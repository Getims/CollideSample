#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxEditor
{
    [System.Serializable]
    public class EditorSettings : ScriptableObject
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private bool _useScrollBarInTabs = true;
        
        [SerializeField]
        [ListDrawerSettings(HideAddButton = true, DraggableItems = false, Expanded = true)]
        private List<MetaTabSettings> _settingsTabs = new List<MetaTabSettings>();

        [Space(10)]
        [SerializeField]
        [ListDrawerSettings(HideAddButton = true, DraggableItems = false, Expanded = true)]
        private List<MetaTabSettings> _metasTabs = new List<MetaTabSettings>();

        [SerializeField, HideInInspector]
        private string _tabMeta;

        [SerializeField, HideInInspector]
        private int _objectMeta = -1;

        [SerializeField, HideInInspector]
        private int _metaToolBar = -1;

        [SerializeField, HideInInspector]
        private string _tabSettings;

        [SerializeField, HideInInspector]
        private int _objectSettings = -1;

        [SerializeField, HideInInspector]
        private int _settingsToolBar = -1;

        [SerializeField, HideInInspector]
        private MenuType _menuType = MenuType.None;

        // PROPERTIES: ----------------------------------------------------------------------------

        public List<MetaTabSettings> SettingsTabs => _settingsTabs;
        public List<MetaTabSettings> MetasTabs => _metasTabs;
        public MenuType MenuType => _menuType;
        public bool UseScrollBarInTabs => _useScrollBarInTabs;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public MetaTabSettings GetTab(string name, bool isSettings)
        {
            MetaTabSettings result = null;
            if (isSettings)
            {
                result = _settingsTabs.Find(tab => tab.MetaName == name);
                if (result == null)
                    _settingsTabs.Add(new MetaTabSettings(name));
            }
            else
            {
                result = _metasTabs.Find(tab => tab.MetaName == name);
                if (result == null)
                    _metasTabs.Add(new MetaTabSettings(name));
            }

            return result;
        }

        public void FixList(List<string> names, bool isSettings)
        {
            if (isSettings)
            {
                foreach (var tab in _settingsTabs)
                {
                    if (!names.Contains(tab.MetaName))
                        tab.MetaName = null;
                }

                _settingsTabs.RemoveAll(tab => tab.MetaName == null);
            }
            else
            {
                foreach (var tab in _metasTabs)
                {
                    if (!names.Contains(tab.MetaName))
                        tab.MetaName = null;
                }

                _metasTabs.RemoveAll(tab => tab.MetaName == null);
            }
        }

        public void SaveMetaSuperType(System.Type metaSuperType)
        {
            if (_menuType == MenuType.Meta)
                _tabMeta = metaSuperType.FullName;
            else if (_menuType == MenuType.Settings)
                _tabSettings = metaSuperType.FullName;
        }

        public void SaveToolBarNumber(int number)
        {
            if (_menuType == MenuType.Meta)
                _metaToolBar = number;
            else if (_menuType == MenuType.Settings)
                _settingsToolBar = number;
        }

        public int GetToolBarNumber()
        {
            if (_menuType == MenuType.Meta)
                return _metaToolBar;
            else if (_menuType == MenuType.Settings)
                return _settingsToolBar;

            return -1;
        }

        public ScriptableObjectExtended GetMetaSuperType()
        {
            string typeName = "";
            if (_menuType == MenuType.Meta)
                typeName = _tabMeta;
            else if (_menuType == MenuType.Settings)
                typeName = _tabSettings;

            if (typeName == null || typeName == "")
                return null;

            return (ScriptableObjectExtended) ScriptableObject.CreateInstance(typeName);
        }

        public void SaveMenuType(MenuType type)
        {
            _menuType = type;
        }

        public void SaveObject(int obj)
        {
            if (_menuType == MenuType.Meta)
                _objectMeta = obj;
            else if (_menuType == MenuType.Settings)
                _objectSettings = obj;
        }

        public int GetObject()
        {
            if (_menuType == MenuType.Meta)
                return _objectMeta;
            else if (_menuType == MenuType.Settings)
                return _objectSettings;

            return 0;
        }

        public void SaveSettings()
        {
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
        }

        public void SetTabs(List<MetaTabSettings> tabs)
        {
            if (_menuType == MenuType.Meta)
               _metasTabs = new List<MetaTabSettings>(tabs);
            else if (_menuType == MenuType.Settings)
                _settingsTabs = new List<MetaTabSettings>(tabs);
        }
    }

    [System.Serializable]
    public class MetaTabSettings
    {
        [SerializeField, HideInInspector] public string MetaName = "";

        [SerializeField, LabelText("$MetaName"), ToggleLeft]
        public bool Show = true;

        public MetaTabSettings(string name)
        {
            MetaName = name;
        }
    }
}
#endif