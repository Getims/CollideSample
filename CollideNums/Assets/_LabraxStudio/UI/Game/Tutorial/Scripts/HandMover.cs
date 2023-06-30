using System;
using DG.Tweening;
using LabraxStudio.Game;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    [Serializable]
    internal class HandMover
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Transform _handContainer;

        [SerializeField]
        private float _pathLength = 0;

        [SerializeField]
        private Ease _moveEase;

        [SerializeField]
        private float _moveTime;

        [SerializeField]
        private float _moveDelay;

        [SerializeField]
        private float _fadeTime;

        [SerializeField]
        private float _fadeDelay;

        // FIELDS: -------------------------------------------------------------------

        private Vector3 _startPosition;
        private bool _canMove = false;
        private Vector3 _path;
        private CanvasGroup _canvasGroup;

        private Tweener _moveTW;
        private Tweener _fadeTW;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void StartMove(Vector3 startPosition, Direction swipeDirection, CanvasGroup canvasGroup)
        {
            _moveTW?.Kill();
            _fadeTW?.Kill();
            _canMove = true;
            _startPosition = startPosition;
            _handContainer.position = _startPosition;
            _path = CalculatePath(swipeDirection);
            _canvasGroup = canvasGroup;

            if (_path == _startPosition)
                return;

            Move();
        }

        public void StopMove()
        {
            _moveTW?.Kill();
            _fadeTW?.Kill();
            _canMove = false;
            _handContainer.position = _startPosition;
            if (_canvasGroup != null)
                _canvasGroup.alpha = 0;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Move()
        {
            if (!_canMove)
                return;

            _moveTW?.Kill();
            _fadeTW?.Kill();
            _handContainer.position = _startPosition;
            _fadeTW = _canvasGroup.DOFade(1, _moveDelay)
                .SetEase(Ease.OutQuad);

            _moveTW = _handContainer.DOMove(_path, _moveTime)
                .SetDelay(_moveDelay)
                .SetEase(_moveEase);

            _fadeTW = _canvasGroup.DOFade(0, _fadeTime)
                .SetDelay(_fadeDelay)
                .SetEase(Ease.OutQuad)
                .OnComplete(Move);
        }

        private Vector3 CalculatePath(Direction swipeDirection)
        {
            Vector3 result = _startPosition;

            switch (swipeDirection)
            {
                case Direction.Left:
                    result += new Vector3(-_pathLength, 0, 0);
                    break;
                case Direction.Right:
                    result += new Vector3(_pathLength, 0, 0);
                    break;
                case Direction.Up:
                    result += new Vector3(0, _pathLength, 0);
                    break;
                case Direction.Down:
                    result += new Vector3(0, -_pathLength, 0);
                    break;
                default:
                    result += Vector3.zero;
                    break;
            }

            return result;
        }
    }
}