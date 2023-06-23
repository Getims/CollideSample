using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    internal class MovementEffect : TileEffect
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Transform _rotatePoint;

        // PUBLIC METHODS: -----------------------------------------------------------------------
        
        public void SetDirection(Direction direction)
        {
            int angle = 0;

            switch (direction)
            {
                case Direction.Left:
                    angle = 180;
                    break;
                case Direction.Right:
                    angle = 0;
                    break;
                case Direction.Up:
                    angle = 90;
                    break;
                case Direction.Down:
                    angle = -90;
                    break;
                default:
                    angle = 0;
                    break;
            }
            
            _rotatePoint.localEulerAngles = new Vector3(0,0,angle);
        }
    }
}