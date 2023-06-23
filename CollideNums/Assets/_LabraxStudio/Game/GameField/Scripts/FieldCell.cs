using UnityEngine;

namespace LabraxStudio.Game.GameField
{
    public class FieldCell : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetName(string name)
        {
            gameObject.name = name;
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void DestroySelf()
        {
            if(gameObject!=null)
                Destroy(gameObject);
        }
    }
}