using System;
using System.Collections.Generic;
using LabraxStudio.Meta;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Game.Gates
{
    [Serializable]
    public class GateVisualizer
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private GateBackground _gateBackground;

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

        // FIELDS: -------------------------------------------------------------------
        
        private Color _openedGateColor;
        private Color _closedGateColor;
        private SpriteRenderer _currentGate;
        
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(int spriteIndex, GameFieldSprites gameFieldSprites, Direction direction, int gateType)
        {
            var gatesSprites = gameFieldSprites.GateSprites;
            var numberSprite = GetNumberSprite(spriteIndex, gatesSprites.GatesNumbers);
            SetNumber(numberSprite, direction);

            _gateBackground.SetBackground(gatesSprites.GateVerticalBackground, gatesSprites.GateHorizontalBackground,
                gameFieldSprites.NotPlayableSprites.NotPlayable1,
                direction);
            SetGateLine(gatesSprites, direction, gateType);
        }
        
        public void SetState(bool isLocked)
        {
            var color = isLocked ? _closedGateColor : _openedGateColor;
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
            _openedGateColor = gatesSprites.UnlockedColor;
            _closedGateColor = gatesSprites.LockedColor;
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
            _currentGate.color = _openedGateColor;
        }
        
    }
}