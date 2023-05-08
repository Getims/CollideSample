using I2.Loc;
using UnityEditor;

namespace LabraxStudio.Meta
{
    [CustomEditor(typeof(LocalizationSettingsMeta))]
    public partial class LocalizationSettingsAssetInspector : LocalizationEditor
    {
        SerializedProperty _fileName;
        SerializedProperty _propSource;
        
        void OnEnable()
        {
            var newSource = target as LocalizationSettingsMeta;
            _propSource = serializedObject.FindProperty("mSource");
            _fileName = serializedObject.FindProperty("FileName");

            Custom_OnEnable(newSource.mSource, _propSource);
        }
        
        public override LanguageSourceData GetSourceData()
        {
            return (target as LocalizationSettingsMeta).mSource;
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_fileName);
            base.OnInspectorGUI();
        }
    }
}