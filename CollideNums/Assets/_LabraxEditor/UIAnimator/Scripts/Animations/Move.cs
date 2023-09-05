using DG.Tweening;
using UnityEngine;

namespace LabraxStudio.UiAnimator
{
    [System.Serializable]
    public class Move : Animation
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private bool _useLocalPosition = false;

        [SerializeField]
        private Vector3 _movePosition;

        [SerializeField]
        private MoveType _moveType;

        // FIELDS: -------------------------------------------------------------------

        private RectTransform _rectTransform;
        private Tweener _moveTW;

        private enum MoveType
        {
            AddPosition,
            SetPosition
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void Play()
        {
            _moveTW?.Kill();
            _rectTransform = _transform.GetComponent<RectTransform>();

            if (_rectTransform == null)
                PlayForTransform();
            else
                PlayForRectTransform();
        }

        public override void Stop()
        {
            _moveTW?.Kill();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void PlayForRectTransform()
        {
            var newPosition = (Vector2) _movePosition;

            if (_moveType == MoveType.AddPosition)
            {
                newPosition = _rectTransform.anchoredPosition + (Vector2) _movePosition;
            }

            if (_instant)
            {
                _rectTransform.anchoredPosition = newPosition;
                return;
            }

            _moveTW = _rectTransform.DOAnchorPos(newPosition, _animationTime)
                .SetEase(_animationEase)
                .SetDelay(StartDelay);
        }

        private void PlayForTransform()
        {
            var newPosition = _movePosition;
            var objectPosition = _useLocalPosition ? _transform.localPosition : _transform.position;

            if (_moveType == MoveType.AddPosition)
            {
                newPosition = objectPosition + _movePosition;
            }

            if (_instant)
            {
                if (_useLocalPosition)
                    _transform.localPosition = newPosition;
                else
                    _transform.position = newPosition;
                return;
            }

            if (_useLocalPosition)
                _moveTW = _transform.DOLocalMove(newPosition, _animationTime)
                    .SetEase(_animationEase)
                    .SetDelay(StartDelay);
            else
                _moveTW = _transform.DOMove(newPosition, _animationTime)
                    .SetEase(_animationEase)
                    .SetDelay(StartDelay);
        }
    }
}