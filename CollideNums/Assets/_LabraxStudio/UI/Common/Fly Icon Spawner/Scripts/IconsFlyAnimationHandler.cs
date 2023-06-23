using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UI.Common.IconsSpawner
{
    public class IconsFlyAnimationHandler : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Settings)]
        [SerializeField, MinMaxSlider(minValue: 0, maxValue: 20, showFields: true)]
        private Vector2Int _iconsNumber;
        
        [SerializeField, MinMaxSlider(minValue: 0, maxValue: 1, showFields: true)]
        private Vector2 _iconsSpawnDelay;

        [SerializeField]
        private bool _customFlySettings;

        [SerializeField, Required]
        [ShowIf(nameof(_customFlySettings))]
        private FlySettings _flySettings;

        [Title(References)]
        [SerializeField, Required]
        private Sprite _iconSprite;

        [SerializeField, Required]
        private Transform _startPoint;

        [SerializeField]
        private bool _useStartPointOffset;
        
        [SerializeField]
        [ShowIf(nameof(_useStartPointOffset))]
        private Vector2 _startPointOffset;

        [SerializeField]
        private bool _useStartPointRandomOffset;

        [SerializeField]
        [ShowIf(nameof(_useStartPointRandomOffset))]
        private float _startPointRandomOffset;
        
        [SerializeField, Required]
        private Transform _targetPoint;
        
        [SerializeField]
        private bool _useTargetPointOffset;

        [SerializeField]
        [ShowIf(nameof(_useTargetPointOffset))]
        private Vector2 _targetPointOffset;
        
        // FIELDS: --------------------------------------------------------------------------------

        private const string Settings = "Settings";
        private const string References = "References";
        private const string DebugButtons = "Debug Buttons";

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void StartAnimation() =>
            StartAnimation(_startPoint);

        public async void StartAnimation(Transform startPoint)
        {
            FlyIconSpawner flyIconSpawner = FlyIconSpawner.Instance;
            FlySettings flySettings = _customFlySettings ? _flySettings : null;
            int iconsNumber = Random.Range(_iconsNumber.x, _iconsNumber.y + 1);

            Vector3 startPosition = startPoint.position;
            Vector3 targetPosition = _targetPoint.position;

            if (_useStartPointOffset)
                startPosition += (Vector3)_startPointOffset;

            if (_useTargetPointOffset)
                targetPosition += (Vector3)_targetPointOffset;
            
            for (int i = 0; i < iconsNumber; i++)
            {
                if (_useStartPointRandomOffset)
                {
                    Vector2 startPositionRandomOffset = Random.insideUnitCircle;
                    startPositionRandomOffset *= _startPointRandomOffset;
                    startPosition += (Vector3)startPositionRandomOffset;
                }

                flyIconSpawner.SpawnIcon(_iconSprite, startPosition, targetPosition,
                    onFlyComplete: null, flySettings);

                float randomValue = Random.Range(_iconsSpawnDelay.x, _iconsSpawnDelay.y);
                int delay = (int)(randomValue * 1000);
                await Task.Delay(delay);
            }
        }
        
        public async void StartAnimation(Vector3 startPosition,  Vector3 targetPosition)
        {
            FlyIconSpawner flyIconSpawner = FlyIconSpawner.Instance;
            FlySettings flySettings = _customFlySettings ? _flySettings : null;
            int iconsNumber = Random.Range(_iconsNumber.x, _iconsNumber.y + 1);

            if (_useStartPointOffset)
                startPosition += (Vector3)_startPointOffset;

            if (_useTargetPointOffset)
                targetPosition += (Vector3)_targetPointOffset;
            
            for (int i = 0; i < iconsNumber; i++)
            {
                if (_useStartPointRandomOffset)
                {
                    Vector2 startPositionRandomOffset = Random.insideUnitCircle;
                    startPositionRandomOffset *= _startPointRandomOffset;
                    startPosition += (Vector3)startPositionRandomOffset;
                }

                flyIconSpawner.SpawnIcon(_iconSprite, startPosition, targetPosition,
                    onFlyComplete: null, flySettings);

                float randomValue = Random.Range(_iconsSpawnDelay.x, _iconsSpawnDelay.y);
                int delay = (int)(randomValue * 1000);
                await Task.Delay(delay);
            }
        }
        
        public void StartAnimation(Vector3 targetPosition)
        {
            StartAnimation(_startPoint.position, targetPosition);
        }

        // DEBUG BUTTONS: -------------------------------------------------------------------------

        [Title(DebugButtons)]
        [Button(30), DisableInEditorMode]
        private void DebugStartAnimation() => StartAnimation();
    }
}