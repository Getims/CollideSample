using LabraxStudio.Managers;
using LabraxStudio.UI.Common.Factory;
using UnityEngine;

namespace LabraxStudio.UI
{
    public class UIManager : SharedManager<UIManager>
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField]
        private UIFactory _gameUIFactory;
        
        [SerializeField]
        private UIFactory _menuUIFactory;

        // PROPERTIES: ----------------------------------------------------------------------------

        public UIFactory GameUiFactory => _gameUIFactory;

        public UIFactory MenuUiFactory => _menuUIFactory;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void InitializeGameUI()
        {
            _gameUIFactory.Create(MenuType.CurrenciesBase);
            _gameUIFactory.Create(MenuType.BoostersPanel);
        }
    }
}
