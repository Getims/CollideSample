using UnityEngine;

namespace LabraxStudio.UI.Common.Factory
{
    public interface IUIFactory
    {
        void Create(MenuType menuType);
        void Create(MenuType menuType, Transform parent);
        void Create(MenuType menuType, out GameObject uiObject);
    }
}