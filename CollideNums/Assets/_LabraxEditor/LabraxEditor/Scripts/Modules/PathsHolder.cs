using System.Linq;

#if UNITY_EDITOR
namespace LabraxEditor
{
    public class PathsHolder
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public static string SettingsPath => SETTINGS_PATH;
        public static string EditorSettingsPath => EDITOR_SETTINGS_PATH;

        // FIELDS: --------------------------------------------------------------------------------

        private const string MetaPath = "Assets/_LabraxStudio/Meta/Resources/";
        private const string SETTINGS_PATH = "Assets/_LabraxStudio/Meta/Settings/";
        private const string EDITOR_SETTINGS_PATH = "Assets/_LabraxEditor/LabraxEditor/Temp/EditorSettings.asset";
        private const string ResourcesPath = "Assets/Resources/";
        private const string ResourcesGameDataPath = "Assets/Resources/Game Data/";

        private static readonly string[] ResourcesPathTypes =
        {
            "AllGlobalSettings",
        };
        
        private static readonly string[] ResourcesGameDataPathTypes =
        {
            "SpecialOfferMeta",
        };

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static string GetPath<T>(T meta, MenuType menuType) where T : ScriptableObjectExtended
        {
            string path = "Assets/";

            switch (menuType)
            {
                case MenuType.Settings:
                    path = SETTINGS_PATH;
                    break;

                case MenuType.Meta:
                    path = MetaPath;
                    break;
            }

            if (meta == null)
                return $"{path}";

            string metaName = meta.GetType().Name;

            if (IsResourcesPath(metaName))
                return ResourcesPath;
            
            if (IsResourcesGameDataPath(metaName))
                return ResourcesGameDataPath + metaName + "/";

            return $"{path + meta.GetType().Name}";
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static bool IsResourcesPath(string metaName) =>
            ResourcesPathTypes.Contains(metaName);
        
        private static bool IsResourcesGameDataPath(string metaName) =>
            ResourcesGameDataPathTypes.Contains(metaName);
    }
}
#endif