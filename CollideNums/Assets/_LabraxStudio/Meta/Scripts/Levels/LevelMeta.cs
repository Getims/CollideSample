using LabraxEditor;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
#if UNITY_EDITOR
using LabraxStudio.Editor;

#endif

namespace LabraxStudio.Meta
{
    public class LevelMeta : ScriptableMeta, ISerializationCallbackReceiver
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private string _levelName = "Level";

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
        [FoldoutGroup("LevelTemplate/Brush")]
        [SerializeField]
        private bool _brushMode = false;

        [FoldoutGroup("LevelTemplate/Brush")]
        [ShowIf(nameof(_brushMode))]
        [SerializeField, Range(0, 20)]
        private int _brushSize = 1;

        [FoldoutGroup("LevelTemplate/Brush")]
        [ShowIf(nameof(_brushMode))]
        [SerializeField, Min(0)]
        private int _rightClickSize = 1;

        [FoldoutGroup("LevelTemplate")]
        [Space(20), InfoBox(LevelDrawTip)]
        [OdinSerialize]
        [ShowInInspector]
        [OnValueChanged(nameof(OnLevelMatrixChanged))]
        [TableMatrix(DrawElementMethod = nameof(DrawLevelEnumElement), ResizableColumns = false, SquareCells = true)]
        private int[,] _levelMatrix = new int[3, 3];

        [FoldoutGroup("LevelTemplate")]
        [Space(20)]
        [OdinSerialize]
        [ShowInInspector]
        [DisableContextMenu(true, true)]
        [TableMatrix(DrawElementMethod = nameof(DrawTilesEnumElement), ResizableColumns = false, SquareCells = true,
            IsReadOnly = true)]
        private int[,] _tilesMatrix = new int[3, 3];

        // PROPERTIES: ----------------------------------------------------------------------------

        public string LevelName => _levelName;
        public int Width => width;
        public int Height => height;
        public int[,] LevelMatrix => _levelMatrix;
        public int[,] TilesMatrix => _tilesMatrix;

        // FIELDS: --------------------------------------------------------------------------------

        private int _levelNumber;

        private const string LevelDrawTip =
            "ЛКМ - увеличить.    Пкм - уменьшить." +
            "\nНеигровая ячейка - 0.    Игровая ячейка - 1.     Ворота - 10-15" +
            "\nЯчейка с верхней границей - 2.    Ячейка с нижней границей - 3" +
            "\nЯчейка с левой границей - 4-6.    Ячейка с правой границей - 7-9";

        private string ElementName => $"{_levelNumber}";

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

        private static int DrawTilesEnumElement(Rect rect, int value)
        {
            
#if UNITY_EDITOR
            value = LevelMatrixDrawer.DrawTilesEnumElement(rect, value);
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

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnLevelMatrixChanged()
        {
            width = _levelMatrix.GetLength(0);
            height = _levelMatrix.GetLength(1);

            ResizeSecondMatrix();
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