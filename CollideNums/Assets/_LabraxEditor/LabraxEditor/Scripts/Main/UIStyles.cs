using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;

namespace LabraxEditor
{
    public class UIStyles : MonoBehaviour
    {
        public static OdinMenuStyle GetTreeStyle()
        {
            OdinMenuStyle menuStyle = new OdinMenuStyle();
            menuStyle.SetIconSize(30);
            menuStyle.SetHeight(30);
            return menuStyle;
        }
    }
}
#endif