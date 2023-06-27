using LabraxStudio.UI.Common;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Tasks
{
    public class IncorrectIcon : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField]
        private Pulsation _pulsation;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void Show()
        {
            base.Show();
            _pulsation.StartPulse();
        }

        public override void Hide()
        {
            base.Hide();
            _pulsation.StopPulse();
        }
    }
}