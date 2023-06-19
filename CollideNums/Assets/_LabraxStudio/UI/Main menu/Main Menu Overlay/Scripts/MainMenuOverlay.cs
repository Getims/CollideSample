using LabraxStudio.App.Services;
using LabraxStudio.Events;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.MainMenu
{
    public class MainMenuOverlay : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Button _tapButton;

        // FIELDS: -------------------------------------------------------------------

        private bool _wasClicked = false;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            Show();
            _tapButton.onClick.AddListener(OnTapToStart);
            UIEvents.SendUISelect();
            ServicesAccess.TouchService.SetTouchState(false);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnTapToStart()
        {
            if (_wasClicked)
                return;

            _wasClicked = true;
            Hide();
            DestroySelfDelayed();

            ServicesAccess.TouchService.SetTouchState(true);

            UIEvents.SendUIDeselect();
            UIEvents.SendMainMenuTapToPlay();
        }
    }
}