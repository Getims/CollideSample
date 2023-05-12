namespace LabraxStudio.Game
{
    public enum GameCellType
    {
        Locked = 0,
        Unlocked = 1,
        Gate2 = 2,
        Gate4 = 3,
        Gate8 = 4,
        Gate16 = 5,
        Gate32 = 6,
        Gate64 = 7
    }
    
    public enum Direction
    {
        Null,
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4
    }

    public enum Swipe
    {
        Null = 0,
        OneTile = 1,
        TwoTiles = 2,
        Infinite = 3
    }
}