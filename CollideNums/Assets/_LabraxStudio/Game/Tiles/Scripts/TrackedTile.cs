namespace LabraxStudio.Game.Tiles
{
    public class TrackedTile
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public TrackedTile(Tile tile, bool isMoving = false, float moveTime = -1, float movePosition = 0)
        {
            Tile = tile;
            IsMoving = isMoving;
            MoveTime = moveTime;
            MovePosition = movePosition;
        }

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public Tile Tile { get; }
        public bool IsMoving { get; }
        public float MoveTime { get; }
        public float MovePosition { get; }
    }
}