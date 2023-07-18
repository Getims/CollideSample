using LabraxStudio.Game.Gates.Visual;
using UnityEngine;
using LabraxStudio.Meta.GameField;

namespace LabraxStudio.Game.Gates
{
    public class Gate : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private GateVisualizer _gateVisualizer = new GateVisualizer();

        [SerializeField]
        private GateEffectsController _gateEffectsController;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Vector2Int Cell => _cell;
        public GameCellType GateType => _gateType;

        // FIELDS: -------------------------------------------------------------------

        private Vector2Int _cell = Vector2Int.zero;
        private GameCellType _gateType;
        private bool _isLocked = true;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetupGate(int spriteIndex, GameFieldSprites gameFieldSprites, Direction direction, int gateType)
        {
            _gateVisualizer.Setup(spriteIndex, gameFieldSprites, direction, gateType);
            _isLocked = true;
            _gateEffectsController.Setup(direction);
        }

        public void SetCell(Vector2Int cell)
        {
            _cell = cell;
        }

        public void SetType(GameCellType gateType)
        {
            _gateType = gateType;
        }

        public void SetState(bool isLocked)
        {
            _gateVisualizer.SetState(isLocked);
            if (isLocked && !_isLocked)
                _gateEffectsController.PlayLockGateEffect();

            _isLocked = isLocked;
        }

        public void DestroySelf()
        {
            if (gameObject != null)
                Destroy(gameObject);
        }

        public void PlayPassGateEffect() => _gateEffectsController.PlayPassGateEffect();
    }
}