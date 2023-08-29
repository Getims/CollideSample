using System;
using LabraxStudio.Editor;
using LabraxStudio.Game.GameField;
using LabraxStudio.Meta.Levels.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace LabraxStudio.Meta.Levels
{
    [Serializable]
    public class LevelTemplate
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
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

        [SerializeField, ShowIf(nameof(BrushModeObstacles))]
        [LabelText("Enable Palette")]
        private bool _enableObstaclesPalette;

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
        
        [Title("Left click")]
        [SerializeField, ShowIf(nameof(EnableBrushModeObstacles))]
        [EnumToggleButtons, HideLabel]
        private ObstaclesBrushMode _obstaclesBrushMode;

        [Title("Right click")]
        [SerializeField, ShowIf(nameof(EnableBrushModeObstacles))]
        [EnumToggleButtons, HideLabel]
        private ObstaclesBrushMode _obstaclesBrushModeRight;

        [Space(20), InfoBox(LevelDrawTip)]
        [OdinSerialize]
        [ShowInInspector]
        [OnValueChanged(nameof(OnLevelMatrixChanged))]
        [TableMatrix(DrawElementMethod = nameof(DrawLevelEnumElement), ResizableColumns = false, SquareCells = true)]
        private int[,] _levelMatrix = new int[3, 3];

        // PROPERTIES: ----------------------------------------------------------------------------

        public int Width => width;
        public int Height => height;
        public int[,] LevelMatrix => _levelMatrix;
        private bool BrushModeField => _brushMode == BrushMode.Field;
        private bool BrushModeTiles => _brushMode == BrushMode.Tiles;
        private bool BrushModeObstacles => _brushMode == BrushMode.Obstacles;
        private bool EnableBrushModeField => BrushModeField && _enableFieldPalette;
        private bool EnableBrushModeTiles => BrushModeTiles && _enableTilePalette;
        private bool EnableBrushModeObstacles => BrushModeObstacles && _enableObstaclesPalette;

        private const string LevelDrawTip =
            "ЛКМ - увеличить.    Пкм - уменьшить." +
            "\nБортик - 0.    Игровая ячейка - 1.     Ворота - 2-17.     Тайлы - 18 - 33";

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private int DrawLevelEnumElement(Rect rect, int value)
        {
#if UNITY_EDITOR
            bool isBrushMode = _enableFieldPalette;
            int leftClickSize = (int) _fieldBrushMode;
            int rightClickSize = (int) _fieldBrushModeRight;

            switch (_brushMode)
            {
                case BrushMode.Tiles:
                    GetTilesSettings(ref isBrushMode, ref leftClickSize, ref rightClickSize);
                    break;
                case BrushMode.Obstacles:
                    GetObstaclesSettings(ref isBrushMode, ref leftClickSize, ref rightClickSize);
                    break;
            }
           
            value = LevelMatrixDrawer.DrawLevelEnumElement(rect, value, isBrushMode, _brushMode == BrushMode.Field, leftClickSize,
                rightClickSize);
#endif
            return value;
        }

        private void GetTilesSettings(ref bool isBrushMode, ref int leftClickSize, ref int rightClickSize)
        {
            isBrushMode = _enableTilePalette;
            switch (_tilesBrushMode)
            {
                case TileBrushMode.L:
                    leftClickSize = 0;
                    break;
                case TileBrushMode.O:
                    leftClickSize = 1;
                    break;
                default:
                    leftClickSize = GameConstants.TileStartValue - 1 + (int) _tilesBrushMode;
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
        private void GetObstaclesSettings(ref bool isBrushMode, ref int leftClickSize, ref int rightClickSize)
        {
            isBrushMode = _enableObstaclesPalette;
            switch (_obstaclesBrushMode)
            {
                case ObstaclesBrushMode.L:
                    leftClickSize = 0;
                    break;
                case ObstaclesBrushMode.O:
                    leftClickSize = 1;
                    break;
                default:
                    leftClickSize = GameConstants.ObstaclesStartValue - 1 + (int) _obstaclesBrushMode;
                    break;
            }

            switch (_obstaclesBrushModeRight)
            {
                case ObstaclesBrushMode.L:
                    rightClickSize = 0;
                    break;
                case ObstaclesBrushMode.O:
                    rightClickSize = 1;
                    break;
                default:
                    rightClickSize = GameConstants.ObstaclesStartValue - 1 + (int) _obstaclesBrushMode;
                    break;
            }
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

    }
}