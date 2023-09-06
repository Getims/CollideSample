using System;
using System.Collections.Generic;
using LabraxStudio.Meta.GameField;
using UnityEngine;

namespace LabraxStudio.Game.Gates.Visual
{
    [Serializable]
    public class GateLine : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Transform _gateContainer;


        [SerializeField]
        private SpriteRenderer _gateHorizontal;

        [SerializeField]
        private SpriteRenderer _gateHorizontalBottom1;

        [SerializeField]
        private SpriteRenderer _gateHorizontalBottom2;

        [SerializeField]
        private SpriteRenderer _gateHorizontalBottom3;

        [SerializeField]
        private SpriteRenderer _gateHorizontalBottom4;


        [SerializeField]
        private SpriteRenderer _gateVertical;

        [SerializeField]
        private SpriteRenderer _gateVerticalTop;

        [SerializeField]
        private SpriteRenderer _gateVerticalBottom;

        [SerializeField]
        private SpriteRenderer _gateVerticalCut;

        [SerializeField]
        private List<SpriteRenderer> _highlights;

        [SerializeField]
        private List<SpriteRenderer> _horizontalOpenLine = new List<SpriteRenderer>();

        [SerializeField]
        private List<SpriteRenderer> _verticalOpenLine = new List<SpriteRenderer>();

        // FIELDS: -------------------------------------------------------------------

        private bool _useColor;
        private Color _openedGateColor;
        private Color _closedGateColor;
        private SpriteRenderer _currentGate;
        private Vector3 _left = Vector3.one;
        private Vector3 _right = new Vector3(-1, 1, 1);

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetState(bool isLocked)
        {
            if (_useColor)
            {
                var color = isLocked ? _closedGateColor : _openedGateColor;
                _currentGate.color = color;
            }
            else
            {
                _currentGate.enabled = isLocked;
                SetOpenLineState(!isLocked);
            }
        }

        public void Setup(GatesSprites gatesSprites, Direction direction, int gateType)
        {
            _useColor = gatesSprites.UseColor;
            _openedGateColor = gatesSprites.UnlockedColor;
            _closedGateColor = gatesSprites.LockedColor;

            SetupHighlights(_useColor);
            SetupOpenSprites(_useColor, gatesSprites.HorizontalOpenLine, gatesSprites.VerticalOpenLine);
            SetSprites(gatesSprites);

            switch (direction)
            {
                case Direction.Left:
                case Direction.Right:
                    _gateContainer.localScale = direction == Direction.Left ? _left : _right;
                    _currentGate = _gateVertical;
                    if (gateType == 1)
                        _currentGate = _gateVerticalTop;
                    if (gateType == 2)
                        _currentGate = _gateVerticalBottom;
                    if (gateType == 3)
                        _currentGate = _gateVerticalCut;
                    break;
                case Direction.Up:
                    _currentGate = _gateHorizontalBottom1;

                    if (gateType == 1)
                        _currentGate = _gateHorizontalBottom2;
                    if (gateType == 2)
                        _currentGate = _gateHorizontalBottom3;
                    if (gateType == 3)
                        _currentGate = _gateHorizontalBottom4;
                    break;
                case Direction.Down:
                    _currentGate = _gateHorizontal;
                    break;
                case Direction.Null:
                    _currentGate = _gateVertical;
                    break;
            }

            _currentGate.gameObject.SetActive(true);
            if (_useColor)
                _currentGate.color = _openedGateColor;
        }

        private void SetSprites(GatesSprites gatesSprites)
        {
            _gateHorizontal.sprite = gatesSprites.GateHorizontal;
            _gateHorizontalBottom1.sprite = gatesSprites.GateHorizontalBottom1;
            _gateHorizontalBottom2.sprite = gatesSprites.GateHorizontalBottom2;
            _gateHorizontalBottom3.sprite = gatesSprites.GateHorizontalBottom3;
            _gateHorizontalBottom4.sprite = gatesSprites.GateHorizontalBottom4;
            _gateVertical.sprite = gatesSprites.GateVertical;
            _gateVerticalTop.sprite = gatesSprites.GateVerticalTop;
            _gateVerticalBottom.sprite = gatesSprites.GateVerticalBottom;
            _gateVerticalCut.sprite = gatesSprites.GateVerticalCut;
        }

        private void SetupOpenSprites(bool useColor, Sprite horizontalOpenLineSprite, Sprite verticalOpenLineSprite)
        {
            foreach (var openLine in _horizontalOpenLine)
            {
                openLine.sprite = horizontalOpenLineSprite;
                openLine.enabled = false;
                openLine.gameObject.SetActive(!useColor);
            }
            
            foreach (var openLine in _verticalOpenLine)
            {
                openLine.sprite = verticalOpenLineSprite;
                openLine.enabled = false;
                openLine.gameObject.SetActive(!useColor);
            }
        }

        private void SetupHighlights(bool useColor)
        {
            foreach (var highlight in _highlights)
                highlight.enabled = _useColor;
        }

        private void SetOpenLineState(bool enabled)
        {
            foreach (var openLine in _horizontalOpenLine)
                openLine.enabled = enabled;
            foreach (var openLine in _verticalOpenLine)
                openLine.enabled = enabled;
        }
    }
}