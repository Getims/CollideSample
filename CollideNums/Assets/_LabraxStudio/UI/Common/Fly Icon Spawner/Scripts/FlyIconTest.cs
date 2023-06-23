using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UI.Common.IconsSpawner
{
    public class FlyIconTest : MonoBehaviour
    {
#if UNITY_EDITOR

        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Sprite _icon;

        [SerializeField]
        private bool _customSettings;

        [SerializeField]
        [ShowIf(nameof(_customSettings))]
        private FlySettings _flySettings;

        [SerializeField]
        private Transform _startPoint;

        [SerializeField]
        private Transform _target;

        [SerializeField]
        private bool _reactToMouseClicks = false;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Update()
        {
            if (_reactToMouseClicks)
            {
                if (Input.GetMouseButtonDown(0))
                    SpawnIcon(Input.mousePosition);
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SpawnIcon(Vector3 spawnPosition)
        {
            if (_icon == null || _target == null)
                return;

            FlySettings flySettings = _customSettings ? _flySettings : null;
            FlyIconSpawner.Instance.SpawnIcon(_icon, spawnPosition, _target.position, CompleteDebug);
        }

        private void CompleteDebug()
        {
            Debug.Log("Icons fly was ended");
        }

        [Button]
        private void SpawnIconFromObject()
        {
            if (_icon == null || _target == null)
                return;

            FlySettings flySettings = _customSettings ? _flySettings : null;
            FlyIconSpawner.Instance.SpawnIcon(_icon, _startPoint.position, _target.position, CompleteDebug);
        }
#endif
    }
}