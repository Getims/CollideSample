using System;
using LabraxStudio.Managers;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UI.Common.IconsSpawner
{
    public class FlyIconSpawner : SharedManager<FlyIconSpawner>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Required]
        private Camera _camera;

        [SerializeField, Required]
        private LeanGameObjectPool _iconsPool;

        [SerializeField]
        private FlySettings _defaultSettings;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start()
        {
            if (_camera == null)
                _camera = Camera.main;
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SpawnIcon(Sprite icon, Vector3 startPosition, Vector3 targetPosition, Action onFlyComplete = null,
            FlySettings flySettings = null)
        {
            if (flySettings == null)
                flySettings = _defaultSettings;

            var pooledObject = _iconsPool.Spawn(transform.parent);
            
            if (pooledObject.TryGetComponent(out FlyIcon flyIcon))
            {
                flyIcon.Setup(icon, flySettings);
                flyIcon.PlayAnimation(startPosition, targetPosition);
            }

            _iconsPool.Despawn(pooledObject, flySettings.GetAnimationTime());
        }

        public void SpawnIconAndConvertWorldPointToScreen(Sprite icon, Transform startPoint, Vector3 targetPosition,
            Action onFlyComplete = null, FlySettings flySettings = null)
        {
            if (flySettings == null)
                flySettings = _defaultSettings;

            var pooledObject = _iconsPool.Spawn(transform);
            if (pooledObject.TryGetComponent(out FlyIcon flyIcon))
            {
                flyIcon.Setup(icon, flySettings);
                flyIcon.PlayAnimation(ConvertWorldPointToScreen(startPoint), targetPosition);
            }

            _iconsPool.Despawn(pooledObject, flySettings.GetAnimationTime());
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private Vector3 ConvertWorldPointToScreen(Transform transformPoint) =>
            Camera.main != null ? _camera.WorldToScreenPoint(transformPoint.position) : Vector3.zero;
    }
}