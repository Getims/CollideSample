using System.Collections.Generic;
using LabraxStudio.App.Services;
using LabraxStudio.Managers;
using LabraxStudio.Meta;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class TilesController : SharedManager<TilesController>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        TilesGenerator _tilesGenerator = new TilesGenerator();

        [SerializeField]
        private TilesMover _tilesMover = new TilesMover();

        [SerializeField]
        private TilesMerger _tilesMerger = new TilesMerger();

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
            var moveAction = _tilesMover.CalculateMoveAction(tile, direction, swipe);
            actions.Add(moveAction);
            var mergeAction = _tilesMerger.CheckMerge(tile);

            if (mergeAction == null)
                tilesAnimator.Play(actions, CheckAllMerges);
            else
            {
                actions.Add(mergeAction);
                tilesAnimator.Play(actions, () => CheckChainMerges(mergeAction.MergeTo));
            }
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

        public void DestroyTile(Tile tile)
        {
            _tiles.Remove(tile);
            tile.DestroySelf();
        }

        public void CheckTileValue(Tile tile)
        {
            int newValue = _tilesMatrix[tile.Cell.x, tile.Cell.y];
            tile.SetValue(newValue, _tilesGenerator.GetSprite(newValue));
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
        }

        private void CheckAllMerges()
        {
            _animations--;
            if (_animations < 0)
                _animations = 0;
            
            if(_animations>0)
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
                mergeAction = null;
            }

            if (actions.Count == 0)
                return;

            TilesAnimator tilesAnimator = new TilesAnimator();
            tilesAnimator.Play(actions, CheckAllMerges);
        }

        private void ResetMergeFlag()
        {
            foreach (var tile in _tiles)
                tile.SetMergeFlag(false);
        }
    }
}