using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Meta.Levels;
using LabraxStudio.Sound;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class TilesController : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        TilesGenerator _tilesGenerator = new TilesGenerator();

        [SerializeField]
        private TilesMover _tilesMover = new TilesMover();

        [SerializeField]
        private TilesMerger _tilesMerger = new TilesMerger();

        // PROPERTIES: ----------------------------------------------------------------------------

        public List<Tile> Tiles => _tiles;
        public bool IsAnyTileMove => _isAnyTileMove || _animations > 0;

        // FIELDS: -------------------------------------------------------------------

        private int[,] _tilesMatrix;
        private List<Tile> _tiles = new List<Tile>();
        private int _animations = 0;
        private readonly CurrentTileTracker _currentTileTracker = new CurrentTileTracker();
        private bool _isAnyTileMove = false;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy()
        {
            _currentTileTracker.OnDestroy();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(LevelMeta levelMeta)
        {
            _tilesMatrix = (int[,]) levelMeta.TilesMatrix.Clone();
            _tilesGenerator.Initialize();
            _tiles = _tilesGenerator.GenerateTiles(levelMeta.Width, levelMeta.Height, _tilesMatrix);

            if (levelMeta.ForAdsSettings.OverrideMoveDistance)
                _tilesMover.Initialize(levelMeta.LevelMatrix, _tilesMatrix, levelMeta.ObstaclesMatrix,
                    levelMeta.ForAdsSettings.TileMoveMaxDistance);
            else
                _tilesMover.Initialize(levelMeta.LevelMatrix, _tilesMatrix, levelMeta.ObstaclesMatrix,
                    -1);

            _tilesMerger.Initialize(_tilesMatrix);
            _currentTileTracker.Initialize(levelMeta.ForAdsSettings.LevelForAds);
        }

        public void MoveTile(Tile tile, Direction direction, Swipe swipe)
        {
            _animations++;
            _isAnyTileMove = true;
            TilesAnimator tilesAnimator = new TilesAnimator();
            List<AnimationAction> actions = new List<AnimationAction>();

            MoveAction moveAction = _tilesMover.CalculateMoveAction(tile, direction, swipe);
            actions.Add(moveAction);

            MergeAction mergeAction = _tilesMerger.CheckMerge(tile, direction);

            if (mergeAction == null && moveAction.Obstacle != ObstacleType.Null)
            {
                CollideWithObstacleAction collideWithObstacleAction =
                    new CollideWithObstacleAction(tile, direction, moveAction.ObstaclePosition, moveAction.Obstacle);
                actions.Add(collideWithObstacleAction);
            }

            if (tile.MovedToGate)
            {
                mergeAction = null;
                MoveInGateAction moveInGateAction =
                    new MoveInGateAction(tile, direction, DestroyTileInGate, moveAction.MoveTo);
                actions.Add(moveInGateAction);
            }

            if (mergeAction == null)
                tilesAnimator.Play(actions, onComplete: CheckAllMerges);
            else
            {
                actions.Add(mergeAction);
                tilesAnimator.Play(actions, () =>
                    CheckChainMerges(mergeAction.MergeTo));
            }

            GameEvents.SendPreMoveTile(moveAction);
            //GameEvents.SendTileAction();
        }

        public Tile GetTile(Vector2 cell)
        {
            foreach (var tile in _tiles)
            {
                if (tile.Cell == cell)
                    return tile;
            }

            return null;
        }

        public void UpdateTileValue(Tile tile)
        {
            int newValue = _tilesMatrix[tile.Cell.x, tile.Cell.y];
            tile.SetValue(newValue, _tilesGenerator.GetSprite(newValue),_tilesGenerator.GetHighlightSprite(newValue));
        }

        public void DestroyTile(Tile tile)
        {
            _tiles.Remove(tile);
            tile.DestroySelf();

            if (_tiles.Count > 0)
                return;

            ServicesProvider.GameFlowService.GameOverTracker.CheckForWin();
        }

        public void RemoveAllTiles()
        {
            RemoveAllTiles(new List<Tile>(_tiles));
            _tiles.Clear();
        }

        public async void ChangeTileValue(Tile tile, bool increase)
        {
            int newValue = _tilesMatrix[tile.Cell.x, tile.Cell.y];
            newValue = increase ? newValue + 1 : newValue - 1;
            _tilesMatrix[tile.Cell.x, tile.Cell.y] = newValue;
            tile.PlayMergeEffect();

            if (increase)
                GameSoundMediator.Instance.PlayTileMultiplyByBoosterSFX();
            else
                GameSoundMediator.Instance.PlayTileSplitByBoosterSFX();

            await Task.Delay(200);
            tile.SetValue(newValue, _tilesGenerator.GetSprite(newValue), _tilesGenerator.GetHighlightSprite(newValue));
            await Task.Delay(100);

            MergeAction mergeAction = _tilesMerger.CheckMerge(tile);
            if (mergeAction == null)
                CheckAllMerges();
            else
            {
                TilesAnimator tilesAnimator = new TilesAnimator();
                tilesAnimator.Play(mergeAction, () => CheckChainMerges(mergeAction.MergeTo));
            }

            GameEvents.SendTileAction();
        }

        public bool HasTilesExceptTile(int tileValue)
        {
            foreach (var tile in _tiles)
            {
                if (tile.Value != tileValue)
                    return true;
            }

            return false;
        }

        public TrackedTile GetTrackedTile() => _currentTileTracker.GetTile();

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CheckChainMerges(Tile mergeTo)
        {
            MergeAction mergeAction = _tilesMerger.CheckMerge(mergeTo);

            if (mergeAction == null)
                CheckAllMerges();
            else
            {
                TilesAnimator tilesAnimator = new TilesAnimator();
                tilesAnimator.Play(mergeAction, () => CheckChainMerges(mergeAction.MergeTo));
            }

            GameEvents.SendTileAction();
        }

        private void CheckAllMerges()
        {
            _animations--;
            if (_animations < 0)
                _animations = 0;

            if (_animations > 0)
                return;

            ResetMergeFlag();
            List<AnimationAction> actions = new List<AnimationAction>();
            foreach (var tile in _tiles)
            {
                var mergeAction = _tilesMerger.CheckMerge(tile);
                if (mergeAction == null)
                    continue;

                actions.Add(mergeAction);
            }

            if (actions.Count != 0)
            {
                TilesAnimator tilesAnimator = new TilesAnimator();
                tilesAnimator.Play(actions, CheckAllMerges);

                GameEvents.SendTileAction();
                return;
            }

            _isAnyTileMove = false;
            GameEvents.SendTileMergesComplete();
            CheckForFail();
        }

        private void CheckForFail() => ServicesProvider.GameFlowService.GameOverTracker.CheckForFail();

        private void DestroyTileInGate(Tile tile)
        {
            if (tile == null)
                return;

            _tilesMatrix[tile.Cell.x, tile.Cell.y] = 0;
            DestroyTile(tile);
        }

        private void ResetMergeFlag()
        {
            foreach (var tile in _tiles)
                tile.SetMergeFlag(false);
        }

        private async void RemoveAllTiles(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                if (tile != null)
                    tile.DestroySelf();
                await Task.Delay(1);
            }
        }
    }
}