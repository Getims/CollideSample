using UnityEngine;

namespace LabraxStudio.MainMenu.Camera
{
    public class CameraDebugger : MonoBehaviour
    {
        // FIELDS: -------------------------------------------------------------------
        private Vector3 _A, _B, _C, _D;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDrawGizmos()
        {
            DrawPoints();
            DrawLines();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------
        public void SetPoints(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
        {
            _A = A;
            _B = B;
            _C = C;
            _D = D;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void DrawPoints()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_A, 0.1f);
            
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_B, 0.1f);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_C, 0.1f);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_D, 0.1f);
        }

        private void DrawLines()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(_A, _B);
            Gizmos.DrawLine(_B, _C);
            Gizmos.DrawLine(_C, _D);
            Gizmos.DrawLine(_A, _D);
        }
    }
}