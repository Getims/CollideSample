using System.Collections.Generic;
using System.Threading.Tasks;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Meta.Levels;
using UnityEngine;

namespace LabraxStudio.Game.Gates
{
    public class GatesController : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private GatesGenerator _gatesGenerator = new GatesGenerator();

        // PROPERTIES: ----------------------------------------------------------------------------

        private TilesController TilesController => ServicesProvider.GameFlowService.TilesController;

        // FIELDS: -------------------------------------------------------------------
        
        private List<Gate> _gates = new List<Gate>();
        private Gate _lastPassedGate = null;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            GameEvents.OnTileAction.AddListener(CheckGatesState);
            GameEvents.OnLevelTaskProgress.AddListener(ShowTaskTip);
            GameEvents.OnLevelTaskComplete.AddListener(ShowTaskTip);
        }

        private void OnDestroy()
        {
            GameEvents.OnTileAction.RemoveListener(CheckGatesState);
            GameEvents.OnLevelTaskProgress.RemoveListener(ShowTaskTip);
            GameEvents.OnLevelTaskComplete.RemoveListener(ShowTaskTip);
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            _gatesGenerator.Initialize();
            _lastPassedGate = null;
        }

        public void GenerateGates(LevelMeta levelMeta)
        {
            _gates = _gatesGenerator.GenerateGates(levelMeta.Width, levelMeta.Height, levelMeta.LevelMatrix);
        }

        public void CheckGatesState()
        {
            foreach (var gate in _gates)
                gate.SetState(NeedLock(gate));
        }

        public int GetBiggestGateNumber()
        {
            int maxValue = 0;
            foreach (var gateCell in _gates)
            {
                int gateValue = (int) gateCell.GateType;
                if (gateValue > maxValue)
                    maxValue = gateValue;
            }

            return maxValue;
        }

        public bool HasGate(int value)
        {
            foreach (var gateCell in _gates)
            {
                int gateValue = (int) gateCell.GateType;
                if (gateValue == value)
                    return true;
            }

            return false;
        }

        public void ClearGates()
        {
            RemoveGameField(new List<Gate>(_gates));
            _gates.Clear();
        }

        public void PlayGatePassEffect(Vector2Int gateCell)
        {
            Gate gate = GetGate(gateCell);
            if (gate == null)
                return;

            _lastPassedGate = gate;
            gate.PlayPassGateEffect();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private bool NeedLock(Gate gate)
        {
            var cell = gate.Cell;
            var gateType = gate.GateType;

            if (HasBadTile(cell, gateType, Direction.Left))
                return true;

            if (HasBadTile(cell, gateType, Direction.Up))
                return true;

            if (HasBadTile(cell, gateType, Direction.Right))
                return true;

            if (HasBadTile(cell, gateType, Direction.Down))
                return true;

            return false;
        }

        private bool HasBadTile(Vector2Int cell, GameCellType gateType, Direction direction)
        {
            var checkVector = GameTypesConverter.DirectionToVector2Int(direction);
            cell += checkVector;
            var tile = TilesController.GetTile(cell);
            if (tile == null)
                return false;

            var tileGate = GameTypesConverter.TileValueToGateType(tile.Value);
            return tileGate != gateType;
        }

        private async void RemoveGameField(List<Gate> gates)
        {
            foreach (var gateCell in gates)
            {
                gateCell.DestroySelf();
                await Task.Delay(1);
            }
        }

        private Gate GetGate(Vector2Int gateCell)
        {
            return _gates.Find(g => g.Cell == gateCell);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------
        
        private void ShowTaskTip(int tileNumber)
        {
            
        }
    }
}