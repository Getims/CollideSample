namespace LabraxStudio.Game
{
    public static class CellTypeConverter
    {
        public static GameCellType ConvertToType(int cellValue)
        {
            if (cellValue <= 1)
                return GameCellType.Locked;

            if (cellValue <= 10)
                return GameCellType.Unlocked;

            switch (cellValue)
            {
                case 11:
                    return GameCellType.Gate2;
                case 12:
                    return GameCellType.Gate4;
                case 13:
                    return GameCellType.Gate8;
                case 14:
                    return GameCellType.Gate16;
                case 15:
                    return GameCellType.Gate32;
                case 16:
                    return GameCellType.Gate64;
            }

            return GameCellType.Locked;
        }
    }
}