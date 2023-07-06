using DG.Tweening;
using LabraxStudio.Sound;
using LabraxStudio.UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.GameScene.Tasks
{
    public class TaskTip : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Image _tipImage;

        [SerializeField]
        private Pulsation _pulsation;

        [SerializeField]
        private float _fadeTime = 0.25f;

        // FIELDS: -------------------------------------------------------------------

        private Tweener _fadeTW;
        private bool _wasShowed = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Show()
        {
            _fadeTW = _tipImage.DOFade(1, _fadeTime);
            _pulsation.StartPulse();
            if (!_wasShowed)
                UISoundMediator.Instance.PlayClearFieldTipSFX();
            _wasShowed = true;
        }

        public void Hide()
        {
            _fadeTW = _tipImage.DOFade(0, 0);
            _pulsation.StopPulse();
            _wasShowed = false;
        }

        protected void OnDestroy()
        {
            _fadeTW?.Kill();
            _pulsation.StopPulse();
        }
    }
}