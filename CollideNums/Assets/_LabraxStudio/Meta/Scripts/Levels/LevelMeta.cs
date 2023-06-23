using System.Collections.Generic;
using LabraxEditor;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
#if UNITY_EDITOR
using LabraxStudio.Editor;

#endif

namespace LabraxStudio.Meta.Levels
{
    public class LevelMeta : ScriptableMeta, ISerializationCallbackReceiver
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private string _levelName = "Level";

        [SerializeField, Min(0)]
        private int _reward = 0;
        
        #region LevelTemplate

        [FoldoutGroup("LevelTemplate", Expanded = true)]
        [FoldoutGroup("LevelTemplate/Size", Expanded = true)]
        [SerializeField, Min(1)]
        private int width = 3;

        [FoldoutGroup("LevelTemplate/Size")]
        [SerializeField, Min(1)]
        private int height = 3;

        [FoldoutGroup("LevelTemplate/Size")]
        [Button("Resize", 20), GUIColor(1, 0.4f, 0.4f)]
        private void OnTemplateSizeChanged()
        {
            ResizeLevelMatrix();
            ResizeSecondMatrix();
        }

        [FoldoutGroup("LevelTemplate/Brush", Expanded = true)]
        [HorizontalGroup("LevelTemplate/Brush/Horizontal")]
        [VerticalGroup("LevelTemplate/Brush/Horizontal/One")]
        [SerializeField]
        private bool _brushMode = false;
        
        [VerticalGroup("LevelTemplate/Brush/Horizontal/One")]
        [ShowIf(nameof(_brushMode))]
        [SerializeField, Min(0)]
        private int _rightClickSize = 1;
        
        [VerticalGroup("LevelTemplate/Brush/Horizontal/One")]
        [ShowIf(nameof(_brushMode))]
        [SerializeField, Range(0, 17), OnValueChanged(nameof(UpdateBrushSprite))]
        private int _brushSize = 1;
        
        [HorizontalGroup("LevelTemplate/Brush/Horizontal", Width = 80)]
        [VerticalGroup("LevelTemplate/Brush/Horizontal/Two")]
        [ShowIf(nameof(_brushMode))]
        [SerializeField]
        [ReadOnly, PreviewField(ObjectFieldAlignment.Left, Height = 80), HideLabel]
        private Texture _brushSprite;
        
        [FoldoutGroup("LevelTemplate")]
        [VerticalGroup("LevelTemplate/Matrix1", PaddingBottom = 20)]
        [Space(20), InfoBox(LevelDrawTip)]
        [OdinSerialize]
        [ShowInInspector]
        [OnValueChanged(nameof(OnLevelMatrixChanged))]
        [TableMatrix(DrawElementMethod = nameof(DrawLevelEnumElement), ResizableColumns = false, SquareCells = true)]
        private int[,] _levelMatrix = new int[3, 3];

        [FoldoutGroup("LevelTemplate/TilesBrush", Expanded = true)]
        [HorizontalGroup("LevelTemplate/TilesBrush/Horizontal")]
        [VerticalGroup("LevelTemplate/TilesBrush/Horizontal/One", PaddingTop = 20)]
        [SerializeField]
        private bool _tilesBrushMode = false;
        
        [VerticalGroup("LevelTemplate/TilesBrush/Horizontal/One")]
        [ShowIf(nameof(_tilesBrushMode))]
        [SerializeField, Range(0, 15), OnValueChanged(nameof(UpdateTilesBrushSprite))]
        private int _tilesBrushSize = 1;
        
        [HorizontalGroup("LevelTemplate/TilesBrush/Horizontal", Width = 80)]
        [VerticalGroup("LevelTemplate/TilesBrush/Horizontal/Two")]
        [ShowIf(nameof(_tilesBrushMode))]
        [SerializeField]
        [ReadOnly, PreviewField(ObjectFieldAlignment.Left, Height = 80), HideLabel]
        private Texture _tilesBrushSprite;
        
        [FoldoutGroup("LevelTemplate")]
        [Space(20)]
        [OdinSerialize]
        [ShowInInspector]
        [DisableContextMenu(true, true)]
        [TableMatrix(DrawElementMethod = nameof(DrawTilesEnumElement), ResizableColumns = false, SquareCells = true,
            IsReadOnly = true)]
        private int[,] _tilesMatrix = new int[3, 3];

        #endregion

        [SerializeField]
        private List<BoostersSettings> _boostersSettings = new List<BoostersSettings>();
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public string LevelName => _levelName;
        public int Width => width;
        public int Height => height;
        public int[,] LevelMatrix => _levelMatrix;
        public int[,] TilesMatrix => _tilesMatrix;

        public  List<BoostersSettings> BoostersSettings => _boostersSettings;

        // FIELDS: --------------------------------------------------------------------------------

        private int _levelNumber;

        private const string LevelDrawTip =
            "ЛКМ - увеличить.    Пкм - уменьшить." +
            "\nНеигровая ячейка - 0.    Игровая ячейка - 1.     Ворота - 2-10";

        private string ElementName => $"{_levelNumber}";

        public int Reward => _reward;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetLevelNumber(int value) =>
            _levelNumber = value;

        // PRIVATE METHODS: -----------------------------------------------------------------------


        private int DrawLevelEnumElement(Rect rect, int value)
        {
#if UNITY_EDITOR
            value = LevelMatrixDrawer.DrawLevelEnumElement(rect, value, _brushMode, _brushSize, _rightClickSize);
#endif
            return value;
        }

        private int DrawTilesEnumElement(Rect rect, int value)
        {
            
#if UNITY_EDITOR
            value = LevelMatrixDrawer.DrawTilesEnumElement(rect, value,_tilesBrushMode, _tilesBrushSize);
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

        private void ResizeSecondMatrix()
        {
            var oldSecondMatrix = _tilesMatrix;
            int oldSecondMatrixWidth = oldSecondMatrix.GetLength(0);
            int oldSecondMatrixHeight = oldSecondMatrix.GetLength(1);

            _tilesMatrix = new int[width, height];
            int levelWidth = _tilesMatrix.GetLength(0);
            int levelHeight = _tilesMatrix.GetLength(1);

            // Copying data from the previous Second Matrix
            for (int width = 0; width < levelWidth; width++)
            {
                if (width == oldSecondMatrixWidth)
                    break;

                for (int height = 0; height < levelHeight; height++)
                {
                    if (height == oldSecondMatrixHeight)
                        break;

                    _tilesMatrix[width, height] = oldSecondMatrix[width, height];
                }
            }
        }

        [Button]
        private void CheckLevel()
        {
            LevelBot bot = new LevelBot();
            bot.CalculateWinRate(_levelMatrix, _tilesMatrix, width, height);
        }
        
        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnLevelMatrixChanged()
        {
            width = _levelMatrix.GetLength(0);
            height = _levelMatrix.GetLength(1);

            ResizeSecondMatrix();
        }

        private void UpdateBrushSprite()
        {
            _brushSprite = LevelMatrixDrawer.GetBrushTexture(_brushSize);
        }
        
        private void UpdateTilesBrushSprite()
        {
            _tilesBrushSprite = LevelMatrixDrawer.GetTilesBrushTexture(_tilesBrushSize);
        }

        // SAVE FIX: ------------------------------------------------------------------------

        [SerializeField]
        [HideInInspector]
        private SerializationData serializationData;

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            UnitySerializationUtility.DeserializeUnityObject((Object) this, ref this.serializationData,
                (DeserializationContext) null);
            this.OnAfterDeserialize();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.OnBeforeSerialize();
            UnitySerializationUtility.SerializeUnityObject((Object) this, ref this.serializationData, false,
                (SerializationContext) null);
        }

        /// <summary>Invoked after deserialization has taken place.</summary>
        protected virtual void OnAfterDeserialize()
        {
        }

        /// <summary>Invoked before serialization has taken place.</summary>
        protected virtual void OnBeforeSerialize()
        {
        }
    }
}