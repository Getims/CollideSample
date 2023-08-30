using System.Collections.Generic;
using LabraxEditor;
using LabraxStudio.Game.GameField;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LabraxStudio.Meta.Levels
{
    public class LevelMeta : ScriptableMeta, ISerializationCallbackReceiver
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private string _levelName = "Level";

        [SerializeField, Min(0)]
        private int _reward = 0;
        
        [TabGroup("Level template")]
        [SerializeField]
        private Vector2 _cameraOffset = new Vector2(0, 0);

        [TabGroup("Level template")]
        [OdinSerialize]
        [SerializeField, InlineProperty, HideLabel]
        private LevelTemplate _levelTemplate = new LevelTemplate();

        [TabGroup("Boosters settings")]
        [SerializeField, InlineProperty, HideLabel]
        private List<BoostersSettings> _boostersSettings = new List<BoostersSettings>();

        [TabGroup("Tasks settings")]
        [SerializeField, InlineProperty, HideLabel]
        private TaskSettings _taskSettings;

        [TabGroup("For ads settings")]
        [SerializeField, InlineProperty, HideLabel]
        private ForAdsSettings forAdsSettings;

        // PROPERTIES: ----------------------------------------------------------------------------

        public string LevelName => _levelName;
        public Vector2 CameraOffset => _cameraOffset;
        public int Width => _levelTemplate.Width;
        public int Height => _levelTemplate.Height;
        public int[,] LevelMatrix => GetLevelMatrix();
        public int[,] TilesMatrix => GetTileMatrix();
        public int[,] ObstaclesMatrix => GetObstaclesMatrix();
        public List<BoostersSettings> BoostersSettings => _boostersSettings;
        public int Reward => _reward;
        public TaskSettings TaskSettings => _taskSettings;
        public ForAdsSettings ForAdsSettings => forAdsSettings;

        // FIELDS: --------------------------------------------------------------------------------

        private int _levelNumber;
        private string ElementName => $"{_levelNumber}";

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetLevelNumber(int value) =>
            _levelNumber = value;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private int[,] GetLevelMatrix()
        {
            int[,] levelMatrix = new int[_levelTemplate.Width, _levelTemplate.Height];
            for (int i = 0; i < _levelTemplate.Width; i++)
            {
                for (int j = 0; j < _levelTemplate.Height; j++)
                {
                    int tile = _levelTemplate.LevelMatrix[i, j];
                    if (tile > GameConstants.TileStartValue)
                        tile = 2; 
                    levelMatrix[i, j] = tile;
                }
            }

            return levelMatrix;
        }

        private int[,] GetTileMatrix()
        {
            int[,] tileMatrix = new int[_levelTemplate.Width, _levelTemplate.Height];
            for (int i = 0; i < _levelTemplate.Width; i++)
            {
                for (int j = 0; j < _levelTemplate.Height; j++)
                {
                    int tile = 0;
                    if(_levelTemplate.LevelMatrix[i, j]< GameConstants.ObstaclesStartValue)
                        tile = _levelTemplate.LevelMatrix[i, j] - GameConstants.TileStartValue;
                    
                    tile = tile < 0 ? 0 : tile;
                    tileMatrix[i, j] = tile;
                }
            }

            return tileMatrix;
        }
        
        private int[,]GetObstaclesMatrix()
        {
            int[,] obstaclesMatrix = new int[_levelTemplate.Width, _levelTemplate.Height];
            for (int i = 0; i < _levelTemplate.Width; i++)
            {
                for (int j = 0; j < _levelTemplate.Height; j++)
                {
                    int obstacle = _levelTemplate.LevelMatrix[i, j] - GameConstants.ObstaclesStartValue;
                    obstacle = obstacle < 0 ? 0 : obstacle;
                    obstaclesMatrix[i, j] = obstacle;
                }
            }

            return obstaclesMatrix;
        }

        // SAVE FIX: ------------------------------------------------------------------------

        [SerializeField]
        [HideInInspector]
        private SerializationData serializationData;

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            UnitySerializationUtility.DeserializeUnityObject((Object) this, ref this.serializationData,
                (DeserializationContext) null);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            UnitySerializationUtility.SerializeUnityObject((Object) this, ref this.serializationData, false,
                (SerializationContext) null);
        }
    }
}