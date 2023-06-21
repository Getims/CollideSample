using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    internal class TileEffect : SpineAnimation
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private bool _checkRenderer = false;

        [SerializeField, ShowIf(nameof(_checkRenderer))]
        private Renderer _spriteRenderer;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void PlayAnimation()
        {
            if (_checkRenderer)
                _spriteRenderer.enabled = true;

            base.PlayAnimation();
        }
    }
}