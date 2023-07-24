using System;
using System.Collections.Generic;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class CurrentTileTracker
    {
        // FIELDS: -------------------------------------------------------------------

        private TrackedTile _trackedTile = new TrackedTile(null);
        private float _cellSize;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            OnDestroy();
            GameEvents.OnTileAction.AddListener(OnTileAction);
            GameEvents.OnPreMoveTile.AddListener(OnPreMoveTile);
            GameEvents.OnPreMergeTile.AddListener(OnPreMergeTile);
            _cellSize = ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSettings.CellSize;
        }

        public TrackedTile GetTile()
        {
            return _trackedTile;
        }

        public void OnDestroy()
        {
            GameEvents.OnTileAction.RemoveListener(OnTileAction);
            GameEvents.OnPreMoveTile.RemoveListener(OnPreMoveTile);
            GameEvents.OnPreMergeTile.AddListener(OnPreMergeTile);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CheckTile(MoveAction moveAction)
        {
            float avgPosition = 0;
            Tile tile = null;

            if (moveAction != null)
            {
                tile = moveAction.Tile;
                var time = moveAction.GetTime();
                _trackedTile = new TrackedTile(tile, true, time, CalculatePosition(moveAction));
            }
            else
            {
                if (_trackedTile.Tile == null)
                {
                    List<Tile> tiles = ServicesProvider.GameFlowService.TilesController.Tiles;
                    tile = CalculateTile(tiles);
                }
                else
                    tile = _trackedTile.Tile;

                _trackedTile = new TrackedTile(tile, false);
            }

            GameEvents.SendTrackedTileUpdate(_trackedTile);
        }

        private void CheckTileMerge(MergeAction mergeAction)
        {
            if (mergeAction == null)
                return;

            _trackedTile = new TrackedTile(mergeAction.MergeTo, false);
            GameEvents.SendTrackedTileUpdate(_trackedTile);
        }

        private Tile CalculateTile(List<Tile> tiles)
        {
            float minPosition = 1000;
            UnityEngine.Camera camera = ServicesProvider.GameFlowService.CameraController.Camera;
            float tileOffset = _cellSize;
            float cameraPosition = camera.transform.position.y + tileOffset;
            Tile result = _trackedTile.Tile;

            foreach (var tile in tiles)
            {
                if (tile != null)
                {
                    float tilePosition = tile.Position.y;
                    float distance = Math.Abs(cameraPosition - tilePosition);
                    if (distance < minPosition)
                    {
                        minPosition = distance;
                        result = tile;
                    }
                }
            }

            return result;
        }

        private float CalculatePosition(MoveAction moveAction)
        {
            Vector2 matrixToPosition =
                GameTypesConverter.MatrixPositionToGamePosition(moveAction.MoveTo, _cellSize);

            return matrixToPosition.y;
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnTileAction() => CheckTile(null);

        private void OnPreMoveTile(MoveAction moveAction) => CheckTile(moveAction);

        private void OnPreMergeTile(MergeAction mergeAction) => CheckTileMerge(mergeAction);
    }
}