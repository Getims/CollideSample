using System.Collections.Generic;
using UnityEngine;

namespace LabraxStudio.Game.Obstacles
{
    public class FrameAnimator : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField]
        private SpriteRenderer _renderer;

        [SerializeField]
        private List<Sprite> sprites;

        [SerializeField]
        public bool loop;

        [SerializeField]
        public bool animate;

        [SerializeField]
        public float frameLength = 1f;

        // FIELDS: -------------------------------------------------------------------

        private float _frameTime;
        public int animationFrame;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start()
        {
            SyncSprite();
        }

        private void LateUpdate()
        {
            if (animate)
            {
                _frameTime += Time.deltaTime;

                while (_frameTime >= frameLength)
                    NextFrame();
            }
        }

        private void NextFrame()
        {
            _frameTime -= frameLength;

            if (animationFrame == sprites.Count - 1)
            {
                if (loop)
                    animationFrame = 0;
                else
                    animate = false;
            }
            else
            {
                animationFrame++;
            }

            SyncSprite();
        }

        private void SyncSprite()
        {
            _renderer.sprite = sprites[animationFrame];
        }
    }
}