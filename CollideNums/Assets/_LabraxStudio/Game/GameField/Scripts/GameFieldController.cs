using System.Collections.Generic;
using System.Threading.Tasks;
using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Game.GameField
{
    public class GameFieldController : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private GameFieldGenerator _gameFieldGenerator = new GameFieldGenerator();

        // FIELDS: -------------------------------------------------------------------

        private List<FieldCell> _cells = new List<FieldCell>();

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            _gameFieldGenerator.Initialize();
        }

        public void GenerateField(LevelMeta levelMeta)
        {
            _cells = _gameFieldGenerator.GenerateFieldSprites(levelMeta.Width, levelMeta.Height, levelMeta.LevelMatrix);
        }

        public void ClearField()
        {
            RemoveGameField(new List<FieldCell>(_cells));
            _cells.Clear();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async void RemoveGameField(List<FieldCell> gameField)
        {
            int step = 0;
            int objectsPerStep = 15;
            foreach (var fieldCell in gameField)
            {
                fieldCell.DestroySelf();
                step++;
                if (step >= objectsPerStep)
                {
                    step = 0;
                    await Task.Delay(1);
                }
            }
        }
    }
}