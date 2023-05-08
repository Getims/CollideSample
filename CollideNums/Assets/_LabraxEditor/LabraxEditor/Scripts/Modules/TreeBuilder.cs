#if UNITY_EDITOR
namespace LabraxEditor
{
    using Sirenix.OdinInspector.Editor;

    public class TreeBuilder
    {
        public static OdinMenuTree BuildSettingsTree()
        {
            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: false, UIStyles.GetTreeStyle());
            tree.AddAllAssetsAtPath(null, PathsHolder.SettingsPath, true, true);

            return tree;
        }

        public static OdinMenuTree BuildMetaTree<T>(T objectExt, MenuType menuType) where T : ScriptableObjectExtended
        {
            if (objectExt == null)
                return null;

            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: false, UIStyles.GetTreeStyle());
            tree.AddAllAssetsAtPath(null, PathsHolder.GetPath(objectExt, menuType), objectExt.GetType(), true, false);
            tree.SortMenuItemsByName();

            return tree;
        }
    }
}
#endif