using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class Tile : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Vector2Int Cell => _cell;

        public int Value => _value;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        // FIELDS: -------------------------------------------------------------------

        private Vector2Int _cell = Vector2Int.zero;
        private int _value;
        private TileSwipeChecker _swipeChecker = new TileSwipeChecker();

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(string name, Sprite sprite)
        {
            gameObject.name = name;
            _spriteRenderer.sprite = sprite;
            _swipeChecker.Initialize(this, UnityEngine.Camera.main, OnSwipe);
        }

        public void SetCell(Vector2Int cell)
        {
            _cell = cell;
        }

        public void SetValue(int value)
        {
            _value = value;
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public void OnSelecet() => _swipeChecker.OnSelect();
        public void OnDeselect() => _swipeChecker.OnDeselect();

        private void OnSwipe(Direction direction, Swipe swipe, float swipeSpeed)
        {
            TilesController.Instance.MoveTile(this, direction, swipe, swipeSpeed);
        }
    }
}