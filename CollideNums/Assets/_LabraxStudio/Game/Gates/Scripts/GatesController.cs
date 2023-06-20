using System.Collections.Generic;
using System.Threading.Tasks;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Meta;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Game.Gates
{
    public class GatesController : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private GatesGenerator _gatesGenerator = new GatesGenerator();

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public List<GateCell> Gates => _gates;
        private TilesController TilesController => ServicesProvider.GameFlowService.TilesController;
        
        // FIELDS: -------------------------------------------------------------------
        private List<GateCell> _gates = new List<GateCell>();

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            GameEvents.OnTileAction.AddListener(CheckGatesState);
        }

        private void OnDestroy()
        {
            GameEvents.OnTileAction.RemoveListener(CheckGatesState);
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            _gatesGenerator.Initialize();
        }

        public void GenerateGates(LevelMeta levelMeta)
        {
            _gates = _gatesGenerator.GenerateGates(levelMeta.Width, levelMeta.Height, levelMeta.LevelMatrix);
        }

        [Button]
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
            RemoveGameField(new List<GateCell>(_gates));
            _gates.Clear();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private bool NeedLock(GateCell gate)
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

        private async void RemoveGameField(List<GateCell> gates)
        {
            foreach (var gateCell in gates)
            {
                gateCell.DestroySelf();
                await Task.Delay(1);
            }
        }
    }
}