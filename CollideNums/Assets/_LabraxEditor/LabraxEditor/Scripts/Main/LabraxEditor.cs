using LabraxEditor.Data;

#if UNITY_EDITOR
namespace LabraxEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEditor;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities.Editor;
    using Sirenix.Utilities;

#pragma warning disable CS0618

    public class LabraxEditor : OdinEditorWindow
    {
        [SerializeField, HideInInspector] private List<string> selectedItems = new List<string>();
        [SerializeField, HideInInspector] private float menuWidth = 180f;
        [SerializeField] private DataEditPanel _testData = new DataEditPanel();
        [SerializeField] private SettingsEditPanel _settingsPanel = new SettingsEditPanel();
        [SerializeField, HideInInspector] private MenuType _menuType = MenuType.None;

        [NonSerialized] private bool isDirty;

        private object _trySelectObject;
        private string _panelTitle = LabraxEditorConstants.BlankPanelTitle;
        private OdinMenuTree _customTree;
        private EditorSettings _settings = null;
        private Vector2 _scrollPosition;
        private ScriptableObjectExtended _metaSuperType;

        #region Editor

        //[MenuItem("Spider Ops/Labrax Editor")]
        public static void OpenWindow()
        {
            var window = GetWindow<LabraxEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        protected override void OnGUI()
        {
            if (_settings == null)
                GetSettings();

            if (_menuType == MenuType.None)
            {
                _menuType = _settings.MenuType;
                if (_menuType == MenuType.None)
                    OnDataClick();
            }

            EditorGUILayout.BeginHorizontal();
            DrawLeftMenu();
            DrawMainWindow();
            EditorGUILayout.EndHorizontal();
        }

        private void OnDisable()
        {
            if (_settings != null)
            {
                _settings.SaveSettings();
            }
        }

        protected override IEnumerable<object> GetTargets()
        {
            switch (_menuType)
            {
                case MenuType.Data:
                    yield return _testData;
                    break;
                case MenuType.EditorSettings:
                    yield return _settingsPanel;
                    break;
                case MenuType.Meta:
                case MenuType.Settings:

                    if (_customTree == null)
                    {
                        yield break;
                    }

                    for (int i = 0; i < _customTree.Selection.Count; i++)
                    {
                        OdinMenuItem odinMenuItem = _customTree.Selection[i];
                        if (odinMenuItem != null)
                        {
                            object obj = odinMenuItem.Value;
                            Func<object> func = obj as Func<object>;
                            if (func != null)
                            {
                                obj = func();
                            }

                            if (obj != null)
                            {
                                yield return obj;
                            }
                        }
                    }

                    break;
            }
        }

        private void ProjectWindowChanged()
        {
            isDirty = true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (UnityEditorEventUtility.HasOnProjectChanged)
            {
                UnityEditorEventUtility.OnProjectChanged -= ProjectWindowChanged;
                UnityEditorEventUtility.OnProjectChanged -= ProjectWindowChanged;
            }
            else
            {
                EditorApplication.projectWindowChanged = (EditorApplication.CallbackFunction) Delegate.Remove(
                    EditorApplication.projectWindowChanged,
                    new EditorApplication.CallbackFunction(ProjectWindowChanged));
                EditorApplication.projectWindowChanged = (EditorApplication.CallbackFunction) Delegate.Remove(
                    EditorApplication.projectWindowChanged,
                    new EditorApplication.CallbackFunction(ProjectWindowChanged));
            }
        }

        #endregion

        #region LeftMenu

        private void DrawLeftMenu()
        {
            Texture icon;

            EditorGUILayout.BeginVertical("box");
            {
                if (_menuType == MenuType.Settings)
                    icon = EditorIcons.SettingsCog.Highlighted;
                else
                    icon = EditorIcons.SettingsCog.Active;

                if (SirenixEditorGUI.IconButton(icon, 40, 40))
                    OnSettingsClick();

                if (_menuType == MenuType.Meta)
                    icon = EditorIcons.FileCabinet.Highlighted;
                else
                    icon = EditorIcons.FileCabinet.Active;

                if (SirenixEditorGUI.IconButton(icon, 40, 40))
                    OnMetaClick();

                if (_menuType == MenuType.Data)
                    icon = EditorIcons.SingleUser.Highlighted;
                else
                    icon = EditorIcons.SingleUser.Active;
                if (SirenixEditorGUI.IconButton(icon, 40, 40))
                    OnDataClick();

                if (_menuType == MenuType.EditorSettings)
                    icon = EditorIcons.List.Highlighted;
                else
                    icon = EditorIcons.List.Active;
                if (SirenixEditorGUI.IconButton(icon, 40, 40))
                    OnEditorSettingsClick();
            }
            EditorGUILayout.EndVertical();
        }

        private void OnSettingsClick()
        {
            var lastType = _menuType;
            _menuType = MenuType.Settings;
            _settings.SaveMenuType(_menuType);

            _panelTitle = LabraxEditorConstants.SettingsPanelTitle;
            BuildTree();
            ReassingSelectionEvent();
        }

        private void OnMetaClick()
        {
            var lastType = _menuType;
            _menuType = MenuType.Meta;
            _settings.SaveMenuType(_menuType);

            _panelTitle = LabraxEditorConstants.MetaPanelTitle;
            BuildTree();
            ReassingSelectionEvent();
        }

        private void SetTreeSelectionFromSettings()
        {
            if (_customTree == null || _customTree.MenuItems.Count == 0)
                return;

            if (_settings == null)
            {
                GetSettings();
                if (_settings == null)
                    return;
            }

            var tree = _customTree.EnumerateTree();
            int objHash = _settings.GetObject();
            OdinMenuItem odinMenuItem = null;

            odinMenuItem = tree.FirstOrDefault(x => x != null &&
                                                    x.Value != null && x.Value.GetHashCode() == objHash);

            if (odinMenuItem != null)
            {
                odinMenuItem.GetParentMenuItemsRecursive(includeSelf: false).ForEach(delegate(OdinMenuItem x)
                {
                    x.Toggled = true;
                });
                odinMenuItem.Select();
            }
        }

        private void OnEditorSettingsClick()
        {
            _customTree = null;
            _menuType = MenuType.EditorSettings;
            _settings.SaveMenuType(_menuType);

            if (_settings == null)
                GetSettings();
            _settingsPanel.SetSettings(_settings);

            if (_settings != null)
            {
                var tabs = MetaDirector.GetMetaTabs(MenuType.Settings);
                List<string> names = new List<string>();
                foreach (var tab in tabs)
                    names.Add(tab.TabName);
                _settings.FixList(names, true);

                tabs = MetaDirector.GetMetaTabs(MenuType.Meta);
                names.Clear();
                foreach (var tab in tabs)
                    names.Add(tab.TabName);
                _settings.FixList(names, false);
            }

            _panelTitle = LabraxEditorConstants.TabsPanelTitle;
        }

        private void OnDataClick()
        {
            _customTree = null;
            _menuType = MenuType.Data;
            _settings.SaveMenuType(_menuType);

            _testData.LoadData();
            _panelTitle = LabraxEditorConstants.DataPanelTitle;
        }

        private void BuildTree()
        {
            switch (_menuType)
            {
                case MenuType.Meta:
                case MenuType.Settings:
                    _metaSuperType = _settings.GetMetaSuperType();
                    _customTree =
                        TreeBuilder.BuildMetaTree(_metaSuperType, _menuType);

                    SetTreeSelectionFromSettings();
                    break;
            }
        }

        #endregion

        private void DrawMainWindow()
        {
            EditorGUILayout.BeginVertical();

            GUILayout.BeginVertical();
            {
                DrawTabPanel();
            }
            GUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box");

            #region Vertical
            
            EditorTimeHelper.Time.Update();
            try
            {
                CheckMenuTree();
                GUILayout.BeginHorizontal();
                //----->

                GUILayout.BeginVertical(GUILayoutOptions.Width(menuWidth).ExpandHeight());
                {
                    switch (_menuType)
                    {
                        case MenuType.Meta:
                        case MenuType.Settings:
                        case MenuType.Data:
                            SirenixEditorGUI.Title(_panelTitle, null, TextAlignment.Center, false, true);
                            break;
                    }

                    Rect rect = GUIHelper.GetCurrentLayoutRect();
                    EditorGUI.DrawRect(rect, SirenixGUIStyles.MenuBackgroundColor);
                    rect.xMin = rect.xMax - 4f;
                    rect.xMax += 4f;

                    EditorGUIUtility.AddCursorRect(rect, MouseCursor.ResizeHorizontal);
                    menuWidth += SirenixEditorGUI.SlideRect(rect).x;

                    switch (_menuType)
                    {
                        case MenuType.Meta:
                        case MenuType.Settings:
                            DrawMenuTree();
                            break;
                        case MenuType.Data:
                            DrawDataPanel();
                            break;
                        case MenuType.EditorSettings:
                            DrawEditorSettingsPanel();
                            break;
                    }
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical();
                {
                    Rect currentLayoutRect2 = GUIHelper.GetCurrentLayoutRect();
                    EditorGUI.DrawRect(currentLayoutRect2, SirenixGUIStyles.DarkEditorBackground);

                    base.OnGUI();
                    DrawMetaControlButtons();
                }
                GUILayout.EndVertical();

                //<-----
                GUILayout.EndHorizontal();

                if (_customTree != null)
                    _customTree.HandleKeyboardMenuNavigation();

                this.RepaintIfRequested();
            }
            finally
            {
                EditorTimeHelper.Time.Update();
            }

            #endregion

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndVertical();
        }

        private void DrawDataPanel()
        {
            if (GUILayout.Button("Load Data", GUILayout.MinHeight(30)))
            {
                _testData.LoadData();
            }

            if (GUILayout.Button("Save Data", GUILayout.MinHeight(30)))
            {
                _testData.SaveData();
            }

            if (GUILayout.Button("Clear Data", GUILayout.MinHeight(30)))
            {
                _testData.ClearData();
            }
        }

        private void DrawEditorSettingsPanel()
        {
        }

        private void DrawTabPanel()
        {
            if (_settings == null)
                return;

            bool isSettings = _menuType == MenuType.Settings;
            bool isMeta = _menuType == MenuType.Meta;

            if (!isMeta && !isSettings)
                return;

            var tabs = MetaDirector.GetMetaTabs(_menuType);
            bool hasTabs = tabs != null && tabs.Count > 0;
            if (!hasTabs)
                return;

            MetaTabSettings tabSettings = null;
            bool openSettingsWindow = false;
            bool canDraw = true;
            int toolbarInt = 0;
            int lastToolBarInt = -1;
            lastToolBarInt = _settings.GetToolBarNumber();
            List<string> toolbarStrings = new List<string>();

            foreach (var tab in tabs)
            {
                tabSettings = _settings.GetTab(tab.TabName, isSettings);
                if (tabSettings == null)
                    canDraw = true;
                else
                    canDraw = tabSettings.Show;
                if (canDraw)
                    toolbarStrings.Add(tab.TabName);
            }

            GUILayout.BeginHorizontal();
            {
                if (_settings.UseScrollBarInTabs)
                {
                    GUILayout.BeginHorizontal();
                    {
                        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(40),
                            GUILayout.ExpandHeight(true), GUILayout.MinHeight(0));
                        toolbarInt = GUILayout.Toolbar(lastToolBarInt, toolbarStrings.ToArray());
                        GUILayout.EndScrollView();
                    }
                    GUILayout.EndHorizontal();
                }
                else
                {
                    GUILayout.BeginHorizontal();
                    {
                        float fullWidth = GUIHelper.ContextWidth - 85f;
                        float width = 200f;
                        int count = (int) (fullWidth / width);
                        toolbarInt = GUILayout.SelectionGrid(lastToolBarInt, toolbarStrings.ToArray(), count);
                    }
                    GUILayout.EndHorizontal();
                }

                GUILayout.BeginHorizontal(GUILayout.Width(25));
                {
                    if (SirenixEditorGUI.IconButton(EditorIcons.SettingsCog.Active, 25, 25))
                        openSettingsWindow = true;
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndHorizontal();

            int i = 0;

            foreach (var tab in tabs)
            {
                tabSettings = _settings.GetTab(tab.TabName, isSettings);
                if (tabSettings == null)
                    canDraw = true;
                else
                    canDraw = tabSettings.Show;

                if (!canDraw)
                    continue;

                if (i == toolbarInt && lastToolBarInt != i)
                {
                    _metaSuperType = tab.SuperType;
                    _settings.SaveMetaSuperType(tab.SOType);

                    BuildTree();

                    if (_customTree.MenuItems.Count > 0)
                    {
                        OdinMenuItem odinMenuItem = _customTree.MenuItems[0];
                        if (odinMenuItem != null)
                        {
                            _trySelectObject = odinMenuItem.Value;
                            if (_trySelectObject != null)
                                _settings.SaveObject(_trySelectObject.GetHashCode());
                            else
                                _settings.SaveObject(-1);

                            ForceMenuTreeRebuild();
                        }
                    }

                    if (_metaSuperType == null)
                    {
                        _metaSuperType = _settings.GetMetaSuperType();

                        if (_metaSuperType == null)
                        {
                            _metaSuperType = tab.SuperType;
                            _settings.SaveMetaSuperType(tab.SOType);
                        }

                        BuildTree();
                    }

                    _panelTitle = tab.TabName;
                    lastToolBarInt = i;
                    _settings.SaveToolBarNumber(lastToolBarInt);
                    break;
                }

                i++;
            }

            if (openSettingsWindow)
            {
                EditorSettingsWindow.OpenWindow(_settings);
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
            }
        }

        private void DrawMetaControlButtons()
        {
            if (_menuType != MenuType.Meta && _menuType != MenuType.Settings)
                return;
            if (_customTree == null)
                return;

            ScriptableObjectExtended meta = null;
            if (_customTree.Selection.Count > 0)
            {
                OdinMenuItem odinMenuItem = _customTree.Selection[0];
                if (odinMenuItem != null)
                {
                    object obj = odinMenuItem.Value;
                    meta = obj as ScriptableObjectExtended;
                }
            }

            if (meta != null)
            {
                SirenixEditorGUI.HorizontalLineSeparator(3);
                GUILayout.BeginHorizontal();

                if (GUILayout.Button(new GUIContent(" Save", EditorIcons.TestPassed)))
                    //if (SirenixEditorGUI.MenuButton(1, " Save meta", false, EditorIcons.TestPassed))
                {
                    meta.SaveObject();
                    _trySelectObject = meta;
                    _settings.SaveObject(_trySelectObject.GetHashCode());
                    BuildTree();
                }

                if (GUILayout.Button(" Duplicate"))
                {
                    ScriptableObjectCreator.Duplicate(meta, null, obj =>
                    {
                        obj.FileName += " (Clone)";
                        obj.SaveObject();
                        _trySelectObject = obj;
                        _settings.SaveObject(_trySelectObject.GetHashCode());
                        BuildTree();
                    });
                }

                if (GUILayout.Button(new GUIContent(" Delete", EditorIcons.TestFailed)))
                {
                    UnityEngine.Object uObject = meta as UnityEngine.Object;
                    if (uObject != null)
                    {
                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(uObject));
                        _customTree.Selection.Clear();
                        selectedItems.Clear();
                        _settings.SaveObject(-1);
                        BuildTree();
                        GUIUtility.ExitGUI();
                    }
                }

                GUILayout.EndHorizontal();
            }
        }

        private void GetSettings()
        {
            var path = PathsHolder.EditorSettingsPath;

            var obj = AssetDatabase.LoadAssetAtPath<EditorSettings>(path);
            if (obj == null)
            {
                var newObj = new EditorSettings();
                AssetDatabase.CreateAsset(newObj, path);
                AssetDatabase.Refresh();
                obj = AssetDatabase.LoadAssetAtPath<EditorSettings>(path);
            }

            _settings = obj;
        }

        #region MenuTree

        private void CheckMenuTree()
        {
            if (_menuType == MenuType.Data || _menuType == MenuType.EditorSettings || _menuType == MenuType.None)
                return;
            if (Event.current.type == EventType.Layout)
            {
                bool flag = _customTree == null;

                if (flag || isDirty)
                {
                    ForceMenuTreeRebuild();
                    if (flag)
                    {
                        OdinMenuTree.ActiveMenuTree = _customTree;
                    }

                    if (UnityEditorEventUtility.HasOnProjectChanged)
                    {
                        UnityEditorEventUtility.OnProjectChanged -= ProjectWindowChanged;
                        UnityEditorEventUtility.OnProjectChanged += ProjectWindowChanged;
                    }
                    else
                    {
                        EditorApplication.projectWindowChanged = (EditorApplication.CallbackFunction) Delegate.Remove(
                            EditorApplication.projectWindowChanged,
                            new EditorApplication.CallbackFunction(ProjectWindowChanged));
                        EditorApplication.projectWindowChanged = (EditorApplication.CallbackFunction) Delegate.Combine(
                            EditorApplication.projectWindowChanged,
                            new EditorApplication.CallbackFunction(ProjectWindowChanged));
                    }

                    isDirty = false;
                }

                if (_trySelectObject != null && _customTree != null)
                {
                    OdinMenuItem odinMenuItem = _customTree.EnumerateTree()
                        .FirstOrDefault((OdinMenuItem x) => x.Value == _trySelectObject);
                    if (odinMenuItem != null)
                    {
                        _customTree.Selection.Clear();
                        _trySelectObject = null;
                        odinMenuItem.Select();
                    }
                }
            }
        }

        private void ForceMenuTreeRebuild()
        {
            BuildTree();
            if (_customTree == null)
                return;

            if (selectedItems.Count == 0 && _customTree.Selection.Count == 0)
            {
                OdinMenuItem odinMenuItem =
                    _customTree.EnumerateTree().FirstOrDefault((OdinMenuItem x) => x.Value != null);
                if (odinMenuItem != null)
                {
                    odinMenuItem.GetParentMenuItemsRecursive(includeSelf: false).ForEach(delegate(OdinMenuItem x)
                    {
                        x.Toggled = true;
                    });

                    _settings.SaveObject(odinMenuItem.Value.GetHashCode());
                    odinMenuItem.Select();
                }
            }
            else if (_customTree.Selection.Count == 0 && selectedItems.Count > 0)
            {
                foreach (OdinMenuItem item in _customTree.EnumerateTree())
                {
                    if (selectedItems.Contains(item.GetFullPath()))
                    {
                        item.Select(addToSelection: true);
                    }
                }
            }

            ReassingSelectionEvent();
        }

        private void ReassingSelectionEvent()
        {
            if (_customTree == null)
                return;

            _customTree.Selection.SelectionChanged -= OnSelectionChanged;
            _customTree.Selection.SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged(SelectionChangedType type)
        {
            Repaint();
            GUIHelper.RemoveFocusControl();
            selectedItems = _customTree.Selection.Select((OdinMenuItem x) => x.GetFullPath()).ToList();
            if (_customTree.Selection.SelectedValue != null)
                _settings.SaveObject(_customTree.Selection.SelectedValue.GetHashCode());
            EditorUtility.SetDirty(this);
        }

        private void DrawMenuTree()
        {
            if (_menuType == MenuType.Meta || _menuType == MenuType.Settings)
            {
                string label = _menuType == MenuType.Meta ? "Create meta" : "Create settings";

                if (SirenixEditorGUI.MenuButton(1, label, false, EditorIcons.Plus.Active))
                {
                    var meta = MetaDirector.CreateMeta(_metaSuperType, _menuType);
                    if (meta != null)
                    {
                        _trySelectObject = meta;
                        _settings.SaveObject(_trySelectObject.GetHashCode());
                        BuildTree();
                        GUIUtility.ExitGUI();
                    }
                }
            }

            SirenixEditorGUI.HorizontalLineSeparator(1);
            if (_customTree != null)
            {
                _customTree.DrawSearchToolbar();
                _customTree.DrawMenuTree();
            }
        }

        #endregion
    }
}
#endif