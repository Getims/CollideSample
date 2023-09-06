using UnityEngine;

namespace LabraxStudio.Game.GameField
{
    public class FieldCell : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private int _baseRenderOrder = 0;
        
        [SerializeField]
        private int _lockedRenderOrder = 1;

        [SerializeField]
        private SpriteMask _spriteMask;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetName(string name)
        {
            gameObject.name = name;
        }

        public void SetSprite(Sprite sprite, bool isLocked)
        {
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.sortingOrder = isLocked ? _lockedRenderOrder : _baseRenderOrder;
            _spriteMask.enabled = isLocked;
        }

        public void DestroySelf()
        {
            if(gameObject!=null)
                Destroy(gameObject);
        }
    }
}