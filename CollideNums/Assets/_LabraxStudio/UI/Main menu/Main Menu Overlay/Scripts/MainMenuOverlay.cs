using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.MainMenu
{
    public class MainMenuOverlay : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Button _tapButton;
        
        [SerializeField]
        private Pulsation _tapPulsation;
        
        // FIELDS: -------------------------------------------------------------------

        private bool _wasClicked = false;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            Show();
            _tapButton.onClick.AddListener(OnTapToStart);
            UIEvents.SendUISelect();
            ServicesProvider.TouchService.SetTouchState(false);
            _tapPulsation.StartPulse();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnTapToStart()
        {
            _tapPulsation.StopPulse();
            if (_wasClicked)
                return;

            _wasClicked = true;
            Hide();
            DestroySelfDelayed();

            ServicesProvider.TouchService.SetTouchState(true);

            UIEvents.SendUIDeselect();
            UIEvents.SendMainMenuTapToPlay();
        }
    }
}