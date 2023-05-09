using I2.Loc;
using LabraxStudio.Meta;
using UnityEditor;

namespace LabraxStudio.Localization
{
    [CustomEditor(typeof(LocalizationSettingsMeta))]
    public partial class LocalizationSettingsAssetInspector : LocalizationEditor
    {
        // FIELDS: -------------------------------------------------------------------
        
        SerializedProperty _fileName;
        SerializedProperty _propSource;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        void OnEnable()
        {
            var newSource = target as LocalizationSettingsMeta;
            _propSource = serializedObject.FindProperty("mSource");
            _fileName = serializedObject.FindProperty("FileName");

            Custom_OnEnable(newSource.mSource, _propSource);
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_fileName);
            base.OnInspectorGUI();
        }
        
        // PUBLIC METHODS: -----------------------------------------------------------------------
        
        public override LanguageSourceData GetSourceData()
        {
            return (target as LocalizationSettingsMeta).mSource;
        }
    }
}