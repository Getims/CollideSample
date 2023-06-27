using System.Collections.Generic;
using System.Threading.Tasks;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Meta.Levels;
using Sirenix.OdinInspector;
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

        public int[,] TilesMatrix => _tilesMatrix;
        public List<Tile> Tiles => _tiles;

        // FIELDS: -------------------------------------------------------------------

        [ShowInInspector]
        private int[,] _tilesMatrix;

        private List<Tile> _tiles = new List<Tile>();
        private int _animations = 0;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(LevelMeta levelMeta)
        {
            _tilesMatrix = (int[,]) levelMeta.TilesMatrix.Clone();
            _tilesGenerator.Initialize();
            _tiles = _tilesGenerator.GenerateTiles(levelMeta.Width, levelMeta.Height, _tilesMatrix);

            _tilesMover.Initialize(levelMeta.LevelMatrix, _tilesMatrix);
            _tilesMerger.Initialize(_tilesMatrix);
        }

        public void MoveTile(Tile tile, Direction direction, Swipe swipe, float swipeSpeed)
        {
            _animations++;
            TilesAnimator tilesAnimator = new TilesAnimator();
            List<AnimationAction> actions = new List<AnimationAction>();

            MoveAction moveAction = _tilesMover.CalculateMoveAction(tile, direction, swipe);
            actions.Add(moveAction);
            MergeAction mergeAction = _tilesMerger.CheckMerge(tile, direction);

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

        public void CheckTileValue(Tile tile)
        {
            int newValue = _tilesMatrix[tile.Cell.x, tile.Cell.y];
            tile.SetValue(newValue, _tilesGenerator.GetSprite(newValue));
        }

        public void DestroyTile(Tile tile)
        {
            _tiles.Remove(tile);
            tile.DestroySelf();

            if (_tiles.Count > 0)
                return;

            ServicesProvider.GameFlowService.GameOverTracker.CheckForWin();
        }

        public void ClearTiles()
        {
            RemoveAllTiles(new List<Tile>(_tiles));
            _tiles.Clear();
        }

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
            MergeAction mergeAction = null;
            foreach (var tile in _tiles)
            {
                mergeAction = _tilesMerger.CheckMerge(tile);
                if (mergeAction == null)
                    continue;

                actions.Add(mergeAction);
            }

            if (actions.Count == 0)
            {
                CheckForFail();
                return;
            }

            TilesAnimator tilesAnimator = new TilesAnimator();
            tilesAnimator.Play(actions, CheckAllMerges);

            GameEvents.SendTileAction();
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
                tile.DestroySelf();
                await Task.Delay(1);
            }
        }
    }
}