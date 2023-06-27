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

            _gateNumber.SetNumber(numberSprite, direction);

            _gateBackground.SetBackground(gatesSprites.GateVerticalBackground, gatesSprites.GateHorizontalBackground,
                gameFieldSprites.NotPlayableSprites.NotPlayable1,
                direction);

            _gateLine.Setup(gatesSprites.UnlockedColor, gatesSprites.LockedColor, direction, gateType);
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
    }
}