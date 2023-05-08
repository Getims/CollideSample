using System.Reflection;
using UnityEngine;
using Sirenix.OdinInspector;

namespace LabraxEditor
{
    public class ScriptableObjectExtended : ScriptableObject, System.IComparable<ScriptableObjectExtended>
    {
        [TitleGroup("Info")]
        [BoxGroup("Info/In", showLabel: false)]
        public string FileName;

        public int CompareTo(ScriptableObjectExtended other)
        {
            return 0;
        }

#if UNITY_EDITOR
        public void SaveObject()
        {
            UnityEditor.EditorUtility.SetDirty(this);
            if (ScriptableObjectUtility.GetAssetNameWithoutExtension(this) != this.FileName)
            {
                ScriptableObjectUtility.RenameAsset(this, this.FileName);
            }
            UnityEditor.AssetDatabase.SaveAssets();
        }
#endif
    }
}