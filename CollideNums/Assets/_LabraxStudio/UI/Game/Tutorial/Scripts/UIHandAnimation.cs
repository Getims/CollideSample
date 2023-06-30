using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    public class UIHandAnimation : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [TabGroup("Movement"), SerializeField]
        private float _startY;

        [TabGroup("Movement"), SerializeField]
        private float _targetY;

        [TabGroup("Movement"), SerializeField, Min(0)]
        private float _moveUpTime;

        [TabGroup("Movement"), SerializeField, Min(0)]
        private float _moveDownTime;

        [TabGroup("Movement"), SerializeField]
        private Ease _moveUpEase;

        [TabGroup("Movement"), SerializeField]
        private Ease _moveDownEase;


        [TabGroup("Fade"), SerializeField, Min(0)]
        private float _fadeTime;


        [TabGroup("References"), SerializeField]
        private RectTransform _targetRT;

        [TabGroup("References"), SerializeField]
        private Image _handImg;

        // FIELDS: --------------------------------------------------------------------------------

        private Tweener _fadeTN;
        private Tweener _moveTN;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy()
        {
            StopAnimation();
            _fadeTN.Kill();
            _moveTN.Kill();
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void StartAnimation()
        {
            StopAnimation();
            Movement(true);
            Fade(true);
        }

        public void StopAnimation()
        {
            _fadeTN.Kill();
            _moveTN.Kill();

            _moveTN = _targetRT.DOAnchorPosY(_startY, 0);
            _fadeTN = _handImg.DOFade(0, 0);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Movement(bool up)
        {
            float y = up ? _targetY : _startY;
            float duration = up ? _moveUpTime : _moveDownTime;
            Ease ease = up ? _moveUpEase : _moveDownEase;

            _moveTN = _targetRT.DOAnchorPosY(y, duration)
                .SetEase(ease)
                .OnComplete(() => Movement(!up));
        }

        private void Fade(bool fadeIn)
        {
            _fadeTN.Kill();
            _fadeTN = _handImg.DOFade(fadeIn ? 1 : 0, _fadeTime);
        }

        // DEBUG BUTTONS: -------------------------------------------------------------------------

        [Title("Debug Buttons")]
        [Button(ButtonHeight = 35), GUIColor(0.4f, 0.4f, 1), HideInEditorMode]
        private void DebugStartAnimation() => StartAnimation();

        [Button(ButtonHeight = 35), GUIColor(0.4f, 0.4f, 1), HideInEditorMode]
        private void DebugStopAnimation() => StopAnimation();
    }
}