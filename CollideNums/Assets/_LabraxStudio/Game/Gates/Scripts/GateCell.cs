using System.Collections.Generic;
using UnityEngine;
using LabraxStudio.Meta;
using Sirenix.OdinInspector;

namespace LabraxStudio.Game.Gates
{
    public class GateCell : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private SpriteRenderer _background;

        [Title("Numbers")]
        [SerializeField]
        private SpriteRenderer _number;

        [SerializeField]
        private SpriteRenderer _leftNumber;

        [SerializeField]
        private SpriteRenderer _rightNumber;

        [Title("Gate line")]
        [SerializeField]
        private SpriteRenderer _topGateLine;

        [SerializeField]
        private SpriteRenderer _bottomGateLine;

        [SerializeField]
        private SpriteRenderer _leftGateLine;

        [SerializeField]
        private SpriteRenderer _rightGateLine;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Vector2Int Cell => _cell;
        public GameCellType GateType => _gateType;

        // FIELDS: -------------------------------------------------------------------

        private Color _openedGate;
        private Color _closedGate;
        private SpriteRenderer _currentGate;
        private Vector2Int _cell = Vector2Int.zero;
        private GameCellType _gateType;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetupGate(int spriteIndex, GatesSprites gatesSprites, Direction direction, int gateType)
        {
            var numberSprite = GetNumberSprite(spriteIndex, gatesSprites.GatesNumbers);
            SetNumber(numberSprite, direction);

            if (direction == Direction.Left || direction == Direction.Right)
                _background.sprite = gatesSprites.GateHorizontalBackground;
            else
                _background.sprite = gatesSprites.GateVerticalBackground;

            SetGateLine(gatesSprites, direction, gateType);
        }

        public void SetCell(Vector2Int cell)
        {
            _cell = cell;
        }

        public void SetType(GameCellType gateType)
        {
            _gateType = gateType;
        }

        public void SetState(bool _isLocked)
        {
            var color = _isLocked ? _closedGate : _openedGate;
            _currentGate.color = color;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private Sprite GetNumberSprite(int spriteIndex, List<Sprite> gatesNumbers)
        {
            if (spriteIndex < 0)
                spriteIndex = 0;
            if (spriteIndex >= gatesNumbers.Count)
                spriteIndex = 0;

            return gatesNumbers[spriteIndex];
        }

        private void SetNumber(Sprite sprite, Direction direction)
        {
            _number.sprite = sprite;
            _leftNumber.sprite = sprite;
            _rightNumber.sprite = sprite;

            _number.enabled = direction != Direction.Left && direction != Direction.Right;
            _leftNumber.enabled = direction == Direction.Left;
            _rightNumber.enabled = direction == Direction.Right;
        }

        private void SetGateLine(GatesSprites gatesSprites, Direction direction, int gateType)
        {
            _openedGate = gatesSprites.UnlockedColor;
            _closedGate = gatesSprites.LockedColor;
            Sprite sprite = null;

            switch (direction)
            {
                case Direction.Left:
                    _currentGate = _leftGateLine;
                    sprite = gatesSprites.GateVertical;
                    if (gateType == 1)
                        sprite = gatesSprites.GateVerticalTop;
                    if (gateType == 2)
                        sprite = gatesSprites.GateVerticalBottom;
                    break;
                case Direction.Up:
                    _currentGate = _topGateLine;
                    sprite = gatesSprites.GateHorizontalBottom1;

                    if (gateType == 1)
                        sprite = gatesSprites.GateHorizontalBottom2;
                    if (gateType == 2)
                        sprite = gatesSprites.GateHorizontalBottom3;
                    break;
                case Direction.Right:
                    _currentGate = _rightGateLine;
                    sprite = gatesSprites.GateVertical;

                    if (gateType == 1)
                        sprite = gatesSprites.GateVerticalTop;
                    if (gateType == 2)
                        sprite = gatesSprites.GateVerticalBottom;
                    break;
                case Direction.Down:
                    _currentGate = _bottomGateLine;
                    sprite = gatesSprites.GateHorizontal;
                    break;
                case Direction.Null:
                    _currentGate = _bottomGateLine;
                    sprite = gatesSprites.GateVertical;
                    break;
            }

            _currentGate.enabled = true;
            _currentGate.sprite = sprite;
            _currentGate.color = _openedGate;
        }
    }
}