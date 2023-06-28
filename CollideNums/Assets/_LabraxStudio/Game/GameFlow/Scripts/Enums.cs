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
        Gate64 = 7,
        Gate128 = 8,
        Gate256 = 9,
        Gate512 = 10,
        Gate1K = 11,
        Gate2K = 12,
        Gate4K = 13,
        Gate8K = 14,
        Gate16K = 15,
        Gate32K = 16,
        Gate64K = 17
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
    
    public enum CurrencyType
    {
        None = 0,
        Money = 1
    }

    public enum BoosterType
    {
        LevelRestart = 0,
        Split = 1,
        Multiply = 2
    }
    
    public enum BoosterCost
    {
        Free,
        Money,
        RV
    }

    public enum FailReason
    {
        None,
        NumbersOverflow,
        NoGatesForTiles,
        NotCompleteAllTasks
    }
}