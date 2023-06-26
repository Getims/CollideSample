using System.Collections.Generic;
using LabraxEditor;
using LabraxStudio.Game.GameField;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

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
        [OdinSerialize]
        [SerializeField, InlineProperty, HideLabel]
        private LevelTemplate _levelTemplate = new LevelTemplate();

        [TabGroup("Boosters settings")]
        [SerializeField]
        private List<BoostersSettings> _boostersSettings = new List<BoostersSettings>();
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public string LevelName => _levelName;
        public int Width => _levelTemplate.Width;
        public int Height => _levelTemplate.Height;
        public int[,] LevelMatrix => GetLevelMatrix();
        public int[,] TilesMatrix => GetTileMatrix();

        public List<BoostersSettings> BoostersSettings => _boostersSettings;

        // FIELDS: --------------------------------------------------------------------------------

        private int _levelNumber;

        private string ElementName => $"{_levelNumber}";

        public int Reward => _reward;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetLevelNumber(int value) =>
            _levelNumber = value;

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        [TabGroup("Level checker")]
        [Button]
        private void CheckLevel()
        {
            LevelBot bot = new LevelBot();
            //bot.CalculateWinRate(_levelMatrix, _tilesMatrix, width, height);
        }

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
                    int tile = _levelTemplate.LevelMatrix[i, j] - GameConstants.TileStartValue;
                    tile = tile < 0 ? 0 : tile;
                    tileMatrix[i, j] = tile;
                }
            }

            return tileMatrix;
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