using System;
using System.Collections.Generic;
using LabraxStudio.Meta.GameField;
using UnityEngine;

namespace LabraxStudio.Game.Gates.Visual
{
    [Serializable]
    public class GateVisualizer
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private GateBackground _gateBackground;

        [SerializeField]
        private GateNumber _gateNumber;

        [SerializeField]
        private GateLine _gateLine;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(int spriteIndex, GameFieldSprites gameFieldSprites, Direction direction, int gateType)
        {
            var gatesSprites = gameFieldSprites.GateSprites;
            var numberSprite = GetNumberSprite(spriteIndex, gatesSprites.GatesNumbers);

            Direction numberDirection = CalculateDirection(spriteIndex, direction);
            _gateNumber.SetNumber(numberSprite, numberDirection, gatesSprites.NumberBaseColor, gatesSprites.NumberGlowColor);

            _gateBackground.SetBackground(gatesSprites.GateVerticalBackground, gatesSprites.GateHorizontalBackground,
                gameFieldSprites.NotPlayableSprites.HorizontalGateExtension,
                direction);

            _gateLine.Setup( gatesSprites, direction, gateType);
        }

        public void SetState(bool isLocked) => _gateLine.SetState(isLocked);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private Sprite GetNumberSprite(int spriteIndex, List<Sprite> gatesNumbers)
        {
            if (spriteIndex < 0)
                spriteIndex = 0;
            if (spriteIndex >= gatesNumbers.Count)
                spriteIndex = 0;

            return gatesNumbers[spriteIndex];
        }

        private Direction CalculateDirection(int spriteIndex, Direction direction)
        {
            Direction newDirection = direction;
            switch (spriteIndex)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 9:
                case 10:
                case 11:
                case 12:
                    newDirection = Direction.Up;
                    break;
            }

            return newDirection;
        }
    }
}