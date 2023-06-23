using System;
using UnityEngine;

namespace LabraxStudio.Game.Gates.Visual
{
    [Serializable]
    public class GateLine
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Transform _gateContainer;

        [SerializeField]
        private SpriteRenderer _gateVertical;

        [SerializeField]
        private SpriteRenderer _gateVerticalTop;

        [SerializeField]
        private SpriteRenderer _gateVerticalBottom;

        [SerializeField]
        private SpriteRenderer _gateHorizontalBottom1;

        [SerializeField]
        private SpriteRenderer _gateHorizontalBottom2;

        [SerializeField]
        private SpriteRenderer _gateHorizontalBottom3;

        [SerializeField]
        private SpriteRenderer _gateHorizontal;

        // FIELDS: -------------------------------------------------------------------

        private Color _openedGateColor;
        private Color _closedGateColor;
        private SpriteRenderer _currentGate;
        private Vector3 _left = Vector3.one;
        private Vector3 _right = new Vector3(-1, 1, 1);

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetState(bool isLocked)
        {
            var color = isLocked ? _closedGateColor : _openedGateColor;
            _currentGate.color = color;
        }

        public void Setup(Color UnlockedColor, Color LockedColor, Direction direction, int gateType)
        {
            _openedGateColor = UnlockedColor;
            _closedGateColor = LockedColor;

            switch (direction)
            {
                case Direction.Left:
                    _gateContainer.localScale = _left;
                    _currentGate = _gateVertical;
                    if (gateType == 1)
                        _currentGate = _gateVerticalTop;
                    if (gateType == 2)
                        _currentGate = _gateVerticalBottom;
                    break;
                case Direction.Up:
                    _currentGate = _gateHorizontalBottom1;

                    if (gateType == 1)
                        _currentGate = _gateHorizontalBottom2;
                    if (gateType == 2)
                        _currentGate = _gateHorizontalBottom3;
                    break;
                case Direction.Right:
                    _gateContainer.localScale = _right;
                    _currentGate = _gateVertical;
                    if (gateType == 1)
                        _currentGate = _gateVerticalTop;
                    if (gateType == 2)
                        _currentGate = _gateVerticalBottom;
                    break;
                case Direction.Down:
                    _currentGate = _gateHorizontal;
                    break;
                case Direction.Null:
                    _currentGate = _gateVertical;
                    break;
            }

            _currentGate.gameObject.SetActive(true);
            _currentGate.color = _openedGateColor;
        }
    }
}