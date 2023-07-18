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

        // FIELDS: -------------------------------------------------------------------

        private Tweener _glowTw;
        private SpriteRenderer _currentNumber;
        private Color _baseColor = Color.white;
        private Color _glowColor = Color.white;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy()
        {
            _glowTw?.Kill();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void PlayGlow(bool isPass)
        {
            _currentNumber.color = _glowColor;
            float time = isPass ? 0.3f : 0.2f;
            float delay = isPass ? 0.25f : 0.1f;
            
            _glowTw = _currentNumber.DOColor(_baseColor, time)
                .SetDelay(delay);
        }

        public void SetNumber(Sprite sprite, Direction direction, Color numberBaseColor, Color numberGlowColor)
        {
            _baseColor = numberBaseColor;
            _glowColor = numberGlowColor;
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

    }
}