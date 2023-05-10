using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Game.GameField
{
    public class GameFieldController : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private GameFieldGenerator _gameFieldGenerator = new GameFieldGenerator();

        // PROPERTIES: ----------------------------------------------------------------------------

        public int[,] GameField => _gameField;

        // FIELDS: -------------------------------------------------------------------

        private int[,] _gameField;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(LevelMeta levelMeta)
        {
            _gameFieldGenerator.Initialize();
            _gameFieldGenerator.GenerateFieldSprites(levelMeta.Width, levelMeta.Height, levelMeta.LevelMatrix);
            _gameField = levelMeta.LevelMatrix;
        }
    }
}