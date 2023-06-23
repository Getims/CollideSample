using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace LabraxStudio.MainMenu.Camera
{
    public class MovingElement : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private int _id = 0;

        [SerializeField]
        private Transform _moveElement;

        [SerializeField]
        private Vector3 _movePosition;

        [SerializeField]
        private MoveType _moveType = MoveType.AddPosition;

        [SerializeField]
        private float _moveTime = 0.2f;

        [SerializeField]
        private Ease _moveEase = Ease.InOutQuint;

        [SerializeField]
        private float _delay = 0;

        [Tooltip("If enable can't move until reset")]
        [SerializeField]
        private bool _checkState = true;

        [SerializeField]
        private UnityEvent _onMove;

        [SerializeField]
        private UnityEvent _onMoveReturn;

        // FIELDS: -------------------------------------------------------------------

        private bool _isSetuped = false;
        private bool _isUp = false;
        private Vector3 _startPosition;
        private Tweener _moveTW;
        private float _moveEffector = 1;

        // PROPERTIES: ----------------------------------------------------------------------------

        public int Id => _id;
        public float MoveTime => _moveTime;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start()
        {
            if (_isSetuped)
                return;

            _startPosition = _moveElement.localPosition;
            _isSetuped = true;
        }

        private void OnDestroy()
        {
            _moveTW.Kill();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void ResetStartPosition()
        {
            _startPosition = _moveElement.localPosition;
        }

        public void Move(float delay = -1, bool instant = false)
        {
            if (!_isSetuped)
            {
                Start();
            }

            if (_checkState)
            {
                if (_isUp)
                    return;
            }

            float moveDelay = _delay;
            if (delay >= 0)
                moveDelay = delay;

            var time = _moveTime;
            if (instant)
            {
                time = 0;
                moveDelay = 0;
            }

            var newPosition = Vector3.zero;
            if (_moveType == MoveType.AddPosition)
                newPosition = _startPosition + _movePosition * _moveEffector;
            else
                newPosition = _movePosition * _moveEffector;

            _moveTW.Kill();
            _moveTW = _moveElement.DOLocalMove(newPosition, time)
                .SetEase(_moveEase)
                .SetDelay(moveDelay);

            _isUp = !_isUp;
            _onMove.Invoke();
        }

        public void ResetState(float delay = -1, bool instant = false)
        {
            if (!_isSetuped)
            {
                Start();
            }

            _isUp = false;
            _moveTW.Kill();

            float moveDelay = _delay;
            if (delay >= 0)
                moveDelay = delay;

            var time = _moveTime;
            if (instant)
            {
                time = 0;
                moveDelay = 0;
            }

            _moveTW = _moveElement.DOLocalMove(_startPosition, time)
                .SetEase(_moveEase)
                .SetDelay(moveDelay);
            _onMoveReturn.Invoke();
        }

        public void SetMoveEffector(float moveEffector)
        {
            _moveEffector = moveEffector;
        }
        
        private enum MoveType
        {
            SetPosition,
            AddPosition
        }
    }
}