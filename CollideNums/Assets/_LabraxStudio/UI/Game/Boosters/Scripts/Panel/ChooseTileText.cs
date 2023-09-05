using LabraxStudio.UI.Common;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Boosters
{
    public class ChooseTileText : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Pulsation _pulsation;

        // PUBLIC METHODS: -----------------------------------------------------------------------

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