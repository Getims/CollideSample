using System.Collections.Generic;
using UnityEngine;

namespace LabraxStudio.Game.Gates
{
    public class GateBackground : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private SpriteRenderer _background;

        [SerializeField]
        private List<SpriteRenderer> _extraTop;

        [SerializeField]
        private List<SpriteRenderer> _extraBottom;

        [SerializeField]
        private List<SpriteRenderer> _extraLeft;

        [SerializeField]
        private List<SpriteRenderer> _extraRight;
        
        [SerializeField]
        private List<SpriteRenderer> _extraLeftNotPlayable;

        [SerializeField]
        private List<SpriteRenderer> _extraRightNotPlayable;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetBackground(Sprite verticalSprite, Sprite horizontalSprite, Sprite notPlayableHorizontalSprite, Direction direction)
        {
            if (direction == Direction.Left || direction == Direction.Right)
                _background.sprite = horizontalSprite;
            else
                _background.sprite = verticalSprite;

            SetupSprites(_extraTop, direction == Direction.Down, verticalSprite);
            SetupSprites(_extraBottom, direction == Direction.Up, verticalSprite);
            SetupSprites(_extraLeft, direction == Direction.Right, horizontalSprite);
            SetupSprites(_extraRight, direction == Direction.Left, horizontalSprite);
            SetupSprites(_extraLeftNotPlayable, direction == Direction.Right, notPlayableHorizontalSprite);
            SetupSprites(_extraRightNotPlayable, direction == Direction.Left, notPlayableHorizontalSprite);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        private void SetupSprites(List<SpriteRenderer> spritesList, bool enabled, Sprite sprite)
        {
            foreach (var spriteRenderer in spritesList)
            {
                spriteRenderer.sprite = sprite;
                spriteRenderer.enabled = enabled;
            }
        }
    }
}