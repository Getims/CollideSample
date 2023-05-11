using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Game.GameField
{
    public class GameFieldController : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private GameFieldGenerator _gameFieldGenerator = new GameFieldGenerator();

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(LevelMeta levelMeta)
        {
            _gameFieldGenerator.Initialize();
            _gameFieldGenerator.GenerateFieldSprites(levelMeta.Width, levelMeta.Height, levelMeta.LevelMatrix);
        }
    }
}