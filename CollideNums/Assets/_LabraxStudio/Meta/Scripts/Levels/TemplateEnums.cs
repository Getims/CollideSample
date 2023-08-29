namespace LabraxStudio.Meta.Levels.Enums
{
    internal enum BrushMode
    {
        Field = 0,
        Tiles = 1,
        Obstacles = 2
    }

    internal enum FieldBrushMode
    {
        Null = -1,
        L = 0,
        O = 1,
        G2 = 2,
        G4 = 3,
        G8 = 4,
        G16 = 5,
        G32 = 6,
        G64 = 7,
        G128 = 8,
        G256 = 9,
        G512 = 10,
        G1k = 11,
        G2k = 12,
        G4k = 13,
        G8k = 14,
        G16k = 15,
        G32k = 16,
        G64k = 17
    }

    internal enum TileBrushMode
    {
        L = -1,
        O = 0,
        T2 = 1,
        T4 = 2,
        T8 = 3,
        T16 = 4,
        T32 = 5,
        T64 = 6,
        T128 = 7,
        T256 = 8,
        T512 = 9,
        T1k = 10,
        T2k = 11,
        T4k = 12,
        T8k = 13,
        T16k = 14,
        T32k = 15,
        T64k = 16
    }

    internal enum ObstaclesBrushMode
    {
        L = -1,
        O = 0,
        Saw = 1,
        Hole = 2,
        Push = 3,
        Stop = 4
    }
}