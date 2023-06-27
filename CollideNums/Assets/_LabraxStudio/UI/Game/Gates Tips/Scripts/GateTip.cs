using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UI.GameScene
{
    public class GateTip : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Settings)]
        [SerializeField, Min(0)]
        private float _moveTime;

        [SerializeField]
        private float _moveOffsetY;

        [SerializeField]
        private Ease _moveEase;

        [SerializeField, Min(0), Space(5)]
        private float _fadeOutDelay;

        [SerializeField, Min(0)]
        private float _fadeOutTime;

        [SerializeField, Min(0)]
        private Ease _fadeOutEase;

        [Title(References)]
        [SerializeField, Required]
        private RectTransform _rect;

        [SerializeField, Required]
        private CanvasGroup _canvasGroup;

        // PROPERTIES: ----------------------------------------------------------------------------

        public RectTransform Rect => _rect;

        // FIELDS: --------------------------------------------------------------------------------

        private const string Settings = "Settings";
        private const string References = "References";

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start()
        {
            StartAnimation();
            Destroy(gameObject, 2f);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void StartAnimation()
        {
            MoveAnimation();
            FadeAnimation();
        }

        private void MoveAnimation()
        {
            float targetY = _rect.anchoredPosition.y + _moveOffsetY;

            _rect
                .DOAnchorPosY(targetY, _moveTime)
                .SetEase(_moveEase);
        }

        private void FadeAnimation()
        {
            _canvasGroup
                .DOFade(0, _fadeOutTime)
                .SetEase(_fadeOutEase)
                .SetDelay(_fadeOutDelay);
        }
    }
}