using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UI.Common.Factory
{
    public class UIFactory : MonoBehaviour, IUIFactory
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Settings)]
        [SerializeField, ListDrawerSettings(ListElementLabelName = "MenuType")]
        private UIMenuSettings[] _uiMenuSettings;

        // FIELDS: --------------------------------------------------------------------------------

        private const string Settings = "Settings";


        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Create(MenuType menuType)
        {
            UIMenuSettings menuSettings = GetMenuSettings(menuType);
            Instantiate(menuSettings.MenuPrefab, menuSettings.Parent);
        }
        
        public void Create(MenuType menuType, Transform parent)
        {
            UIMenuSettings menuSettings = GetMenuSettings(menuType);
            Instantiate(menuSettings.MenuPrefab, parent);
        }

        public void Create(MenuType menuType, out GameObject uiObject)
        {
            UIMenuSettings menuSettings = GetMenuSettings(menuType);
            uiObject = Instantiate(menuSettings.MenuPrefab, menuSettings.Parent);
        }
        
        public T Create<T>(MenuType menuType)
        {
            UIMenuSettings menuSettings = GetMenuSettings(menuType);
            GameObject uiObject = Instantiate(menuSettings.MenuPrefab, menuSettings.Parent);

            uiObject.TryGetComponent<T>(out var result);
            return result;
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private UIMenuSettings GetMenuSettings(MenuType menuType)
        {
            UIMenuSettings menuSettings =
                _uiMenuSettings.FirstOrDefault(menuSettings => menuSettings.MenuType == menuType);

            if (menuSettings != null)
                return menuSettings;
            
            string errorLog = $"<rb>Menu Settings</rb> not found, menu type: <gb>{menuType}</gb>";
            
            Debug.LogError(errorLog);

            return null;
        }
    }
}