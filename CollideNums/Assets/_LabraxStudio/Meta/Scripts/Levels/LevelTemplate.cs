using System;
using LabraxStudio.Editor;
using LabraxStudio.Game.GameField;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace LabraxStudio.Meta.Levels
{
    [Serializable]
    public class LevelTemplate
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public int Width => width;
        public int Height => height;
        public int[,] LevelMatrix => _levelMatrix;
        private bool BrushModeField => _brushMode == BrushMode.Field;
        private bool BrushModeTiles => _brushMode == BrushMode.Tiles;
        private bool EnableBrushModeField => _brushMode == BrushMode.Field && _enableFieldPalette;

        private bool EnableBrushModeTiles => _brushMode == BrushMode.Tiles && _enableTilePalette;
        // FIELDS: -------------------------------------------------------------------

        [FoldoutGroup("Size", Expanded = true)]
        [SerializeField]
        private int width = 3;

        [FoldoutGroup("Size")]
        [SerializeField]
        private int height = 3;

        [FoldoutGroup("Size")]
        [Button("Resize", 28), GUIColor(1, 0.2f, 0.2f)]
        private void OnTemplateSizeChanged()
        {
            width = width < 1 ? 1 : width;
            height = height < 1 ? 1 : height;
            ResizeLevelMatrix();
        }

        [SerializeField]
        [EnumToggleButtons]
        private BrushMode _brushMode;

        [SerializeField, ShowIf(nameof(BrushModeField))]
        [LabelText("Enable Palette")]
        private bool _enableFieldPalette;

        [SerializeField, ShowIf(nameof(BrushModeTiles))]
        [LabelText("Enable Palette")]
        private bool _enableTilePalette;

        [Title("Left click")]
        [SerializeField, ShowIf(nameof(EnableBrushModeField))]
        [EnumToggleButtons, HideLabel]
        private FieldBrushMode _fieldBrushMode;

        [Title("Right click")]
        [SerializeField, ShowIf(nameof(EnableBrushModeField))]
        [EnumToggleButtons, HideLabel]
        private FieldBrushMode _fieldBrushModeRight;

        [Title("Left click")]
        [SerializeField, ShowIf(nameof(EnableBrushModeTiles))]
        [EnumToggleButtons, HideLabel]
        private TileBrushMode _tilesBrushMode;

        [Title("Right click")]
        [SerializeField, ShowIf(nameof(EnableBrushModeTiles))]
        [EnumToggleButtons, HideLabel]
        private TileBrushMode _tilesBrushModeRight;

        [Space(20), InfoBox(LevelDrawTip)]
        [OdinSerialize]
        [ShowInInspector]
        [OnValueChanged(nameof(OnLevelMatrixChanged))]
        [TableMatrix(DrawElementMethod = nameof(DrawLevelEnumElement), ResizableColumns = false, SquareCells = true)]
        private int[,] _levelMatrix = new int[3, 3];


        private const string LevelDrawTip =
            "ЛКМ - увеличить.    Пкм - уменьшить." +
            "\nБортик - 0.    Игровая ячейка - 1.     Ворота - 2-17.     Тайлы - 18 - 33";

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private int DrawLevelEnumElement(Rect rect, int value)
        {
#if UNITY_EDITOR
            bool isBrushMode = _enableFieldPalette;
            int brushSize = (int) _fieldBrushMode;
            int rightClickSize = (int) _fieldBrushModeRight;

            if (_brushMode == BrushMode.Tiles)
            {
                isBrushMode = _enableTilePalette;
                switch (_tilesBrushMode)
                {
                    case TileBrushMode.L:
                        brushSize = 0;
                        break;
                    case TileBrushMode.O:
                        brushSize = 1;
                        break;
                    default:
                        brushSize = GameConstants.TileStartValue - 1 + (int) _tilesBrushMode;
                        break;
                }

                switch (_tilesBrushModeRight)
                {
                    case TileBrushMode.L:
                        rightClickSize = 0;
                        break;
                    case TileBrushMode.O:
                        rightClickSize = 1;
                        break;
                    default:
                        rightClickSize = GameConstants.TileStartValue - 1 + (int) _tilesBrushModeRight;
                        break;
                }
            }

            value = LevelMatrixDrawer.DrawLevelEnumElement(rect, value, isBrushMode, _brushMode == BrushMode.Field, brushSize,
                rightClickSize);
#endif
            return value;
        }

        private void ResizeLevelMatrix()
        {
            var oldLevelMatrix = _levelMatrix;
            int oldLevelWidth = oldLevelMatrix.GetLength(0);
            int oldLevelHeight = oldLevelMatrix.GetLength(1);

            _levelMatrix = new int[width, height];
            int levelWidth = _levelMatrix.GetLength(0);
            int levelHeight = _levelMatrix.GetLength(1);

            // Copying data from the previous Level Matrix
            for (int width = 0; width < levelWidth; width++)
            {
                if (width == oldLevelWidth)
                    break;

                for (int height = 0; height < levelHeight; height++)
                {
                    if (height == oldLevelHeight)
                        break;

                    _levelMatrix[width, height] = oldLevelMatrix[width, height];
                }
            }
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnLevelMatrixChanged()
        {
#if UNITY_EDITOR
            width = _levelMatrix.GetLength(0);
            height = _levelMatrix.GetLength(1);
#endif
        }

        private enum BrushMode
        {
            Field = 0,
            Tiles = 1
        }

        private enum FieldBrushMode
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

        private enum TileBrushMode
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
    }
}