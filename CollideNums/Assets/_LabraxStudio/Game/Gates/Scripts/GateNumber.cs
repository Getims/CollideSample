using System;
using DG.Tweening;
using UnityEngine;

namespace LabraxStudio.Game.Gates.Visual
{
    public class GateNumber : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField]
        private SpriteRenderer _number;

        [SerializeField]
        private SpriteRenderer _leftNumber;

        [SerializeField]
        private SpriteRenderer _rightNumber;

        [SerializeField]
        private Color _baseColor = Color.white;

        [SerializeField]
        private Color _glowColor = Color.white;

        // FIELDS: -------------------------------------------------------------------

        private Tweener _glowTw;
        private SpriteRenderer _currentNumber;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy()
        {
            _glowTw?.Kill();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void PlayGlow()
        {
            _currentNumber.color = _glowColor;
            _glowTw = _currentNumber.DOColor(_baseColor, 0.2f)
                .SetDelay(0.1f);
        }

        public void SetNumber(Sprite sprite, Direction direction)
        {
            _number.sprite = sprite;
            _leftNumber.sprite = sprite;
            _rightNumber.sprite = sprite;

            _number.enabled = direction != Direction.Left && direction != Direction.Right;
            _leftNumber.enabled = direction == Direction.Left;
            _rightNumber.enabled = direction == Direction.Right;

            if (_number.enabled)
                _currentNumber = _number;
            else
            {
                if (_leftNumber.enabled)
                    _currentNumber = _leftNumber;
                else
                    _currentNumber = _rightNumber;
            }

            SetBaseColor();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetBaseColor()
        {
            _number.color = _baseColor;
            _leftNumber.color = _baseColor;
            _rightNumber.color = _baseColor;
        }

        private void SetGlowColor()
        {
            _number.color = _glowColor;
            _leftNumber.color = _glowColor;
            _rightNumber.color = _glowColor;
        }
    }
}