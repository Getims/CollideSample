using System;
using System.Collections.Generic;
using LabraxStudio.Events;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Meta;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Game.Gates
{
    public class GatesController : MonoBehaviour
    {
        // FIELDS: -------------------------------------------------------------------
        private List<GateCell> _gates = new List<GateCell>();

        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private GatesGenerator _gatesGenerator = new GatesGenerator();

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

        public void Initialize(LevelMeta levelMeta)
        {
            _gatesGenerator.Initialize();
            _gates = _gatesGenerator.GenerateGates(levelMeta.Width, levelMeta.Height, levelMeta.LevelMatrix);
        }

        [Button]
        public void CheckGatesState()
        {
            foreach (var gate in _gates)
                gate.SetState(NeedLock(gate));
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
            var tile = TilesController.Instance.GetTile(cell);
            if (tile == null)
                return false;

            var tileGate = GameTypesConverter.TileValueToGateType(tile.Value);
            return tileGate != gateType;
        }
    }
}