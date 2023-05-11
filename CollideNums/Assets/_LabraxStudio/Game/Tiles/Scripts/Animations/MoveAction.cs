using System;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class MoveAction : AnimationAction
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public MoveAction(Tile tile, Vector2Int moveTo)
        {
            _tile = tile;
            _moveTo = moveTo;
            _gameFieldSettings = ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSettings;
        }

        // FIELDS: -------------------------------------------------------------------
       
        private Tile _tile;
        private Vector2Int _moveTo;
        private GameFieldSettings _gameFieldSettings;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void Play()
        {
            Vector2 matrixToPosition =
                GameTypesConverter.MatrixPositionToGamePosition(_moveTo, _gameFieldSettings.CellSize);
            Vector3 newPosition = _tile.Position;
            newPosition.x = matrixToPosition.x;
            newPosition.y = matrixToPosition.y;

            Ease ease = _gameFieldSettings.MoveEase;
            float time = CalculateTime(_gameFieldSettings.OneCellTime, _tile.Position - newPosition);

            _tile.transform.DOMove(newPosition, time)
                .SetEase(ease);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private float CalculateTime(float oneCellTime, Vector3 moveDelta)
        {
            float x = Math.Abs(moveDelta.x);
            float y = Math.Abs(moveDelta.y);
            float n = 0;

            if (x > 0)
                n = x;

            if (y > 0)
                n = y;

            float time = oneCellTime + (n - 1) * oneCellTime / (n - n * _gameFieldSettings.MoveSlowing);
            Utils.ReworkPoint("Time: " + time);
            return time;
        }
    }
}