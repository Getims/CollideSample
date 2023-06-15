using System;
using Unity.VisualScripting;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    [Serializable]
    public class TilesMerger
    {
        // FIELDS: -------------------------------------------------------------------

        private int[,] _tilesMatrix;
        private int _width;
        private int _height;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(int[,] tilesMatrix)
        {
            _tilesMatrix = tilesMatrix;
            _width = tilesMatrix.GetLength(0);
            _height = tilesMatrix.GetLength(1);
        }

        public MergeAction CheckMerge(Tile tile, Direction direction = Direction.Null)
        {
            Tile otherTile = null;

            if (direction != Direction.Null)
            {
                otherTile = CanMerge(tile, direction);
                if (otherTile != null)
                {
                    tile.SetMergeFlag(true);
                    otherTile.SetMergeFlag(true);
                    return new MergeAction(tile, otherTile);
                }
            }
            
            otherTile = CanMerge(tile, Direction.Right);
            if (otherTile != null)
            {
                tile.SetMergeFlag(true);
                otherTile.SetMergeFlag(true);
                return new MergeAction(tile, otherTile);
            }
            
            otherTile = CanMerge(tile, Direction.Down);
            if (otherTile != null)
            {
                tile.SetMergeFlag(true);
                otherTile.SetMergeFlag(true);
                return new MergeAction(tile, otherTile);
            }
            
            otherTile = CanMerge(tile, Direction.Left);
            if (otherTile != null)
            {
                tile.SetMergeFlag(true);
                otherTile.SetMergeFlag(true);
                return new MergeAction(tile, otherTile);
            }
            
            otherTile = CanMerge(tile, Direction.Up);
            if (otherTile != null)
            {
                tile.SetMergeFlag(true);
                otherTile.SetMergeFlag(true);
                return new MergeAction(tile, otherTile);
            }

            return null;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private Tile CanMerge(Tile tile, Direction direction)
        {
            int x = tile.Cell.x;
            int y = tile.Cell.y;

            Vector2Int mergeVector = GameTypesConverter.DirectionToVector2Int(direction);
            x += mergeVector.x;
            y += mergeVector.y;
            
            if (x<0 || x>=_width ||
                y<0 || y>= _height)
                return null;

            int otherValue = _tilesMatrix[x, y];
            if (otherValue != tile.Value)
                return null;

            Tile otherTile = TilesController.Instance.GetTile(new Vector2(x, y));
            if (otherTile.IsMerging)
                return null;
            
            _tilesMatrix[x, y] = tile.Value + 1;
            _tilesMatrix[tile.Cell.x,tile.Cell.y] = 0;
            return otherTile;
        }
    }
}