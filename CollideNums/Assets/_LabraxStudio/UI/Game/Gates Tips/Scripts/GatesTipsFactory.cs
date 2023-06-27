using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UI.GameScene
{
    public class GatesTipsFactory : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(References)]
        [SerializeField, Required]
        private GateTip _gateTipPrefab;

        // FIELDS: --------------------------------------------------------------------------------

        private const string References = "References";

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Create(Vector3 gateWorldPosition)
        {
            GateTip _gateTip = Instantiate(_gateTipPrefab, transform);
            _gateTip.Rect.position = GetScreenPosition(gateWorldPosition);
        }

        private Vector3 GetScreenPosition(Vector3 worldPosition)
        {
            Camera mainCamera = Camera.main;
            return mainCamera != null ? mainCamera.WorldToScreenPoint(worldPosition) : Vector3.zero;
        }
    }
}