using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace LabraxStudio.Meta.Levels
{
    public class LevelBot
    {
        private List<int> _gatesList = new List<int>();
        private List<int> _tilesList = new List<int>();

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void CalculateWinRate(int[,] levelMatrix, int[,] tilesMatrix, int width, int height)
        {
            CalculateGates(levelMatrix, width, height);
            CalculateTiles(tilesMatrix, width, height);

            int testCount = 10000;
            int winsCount = 0;

            for (int i = 0; i < testCount; i++)
            {
                if (CheckWin(_gatesList, _tilesList))
                    winsCount++;
            }

            float winRate = 1f * winsCount / testCount * 100f;
            Utils.InfoPoint(winRate+"%");
        }

        private bool CheckWin(List<int> gatesList, List<int> tilesList)
        {
            bool isWin = true;
            foreach (var tile in tilesList)
            {
                if (tile == 0)
                    continue;

                if (!gatesList.Contains(tile))
                {
                    isWin = false;
                    break;
                }
            }

            if (isWin)
                return true;

            isWin = true;
            for (int i = 0; i < 10; i++)
            {
                isWin = true;
                List<int> combination = new List<int>(tilesList);

                for (int j = 0; j < Random.Range(1, 10); j++)
                {
                    combination = CreateCombinations(combination);
                }

                foreach (var tile in combination)
                {
                    if (tile == 0)
                        continue;

                    if (!gatesList.Contains(tile))
                    {
                        isWin = false;
                        break;
                    }
                }

                if (isWin)
                    break;
            }

            return isWin;
        }

        private List<int> CreateCombinations(List<int> tilesList)
        {
            List<int> check = new List<int>(tilesList);
            int tilesCount = tilesList.Count;
            if (tilesCount == 1)
                return tilesList;

            for (int i = 0; i < tilesCount - 1; i++)
            {
                for (int j = i + 1; j < tilesCount; j++)
                {
                    if (check[i] == check[j])
                    {
                        if (Utils.RandomBool())
                        {
                            check[i] +=1;
                            check[j] = 0;
                            break;
                        }
                    }
                }
            }

            return check;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CalculateGates(int[,] levelMatrix, int levelWidth, int levelHeight)
        {
            _gatesList = new List<int>();

            for (int i = 0; i < levelWidth; i++)
            {
                for (int j = 0; j < levelHeight; j++)
                {
                    int index = levelMatrix[i, j] - 1;
                    if (index < 2)
                        continue;

                    _gatesList.Add(index - 1);
                }
            }
        }

        private void CalculateTiles(int[,] tilesMatrix, int levelWidth, int levelHeight)
        {
            _tilesList = new List<int>();

            for (int i = 0; i < levelWidth; i++)
            {
                for (int j = 0; j < levelHeight; j++)
                {
                    int index = tilesMatrix[i, j];
                    if (index == 0)
                        continue;

                    _tilesList.Add(index);
                }
            }
        }
    }
}