using LabraxStudio.App.Services;
using LabraxStudio.Meta.GameField;
using UnityEngine;

namespace LabraxStudio.Game.Background
{
    public class BackgroundController : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private SpriteRenderer _backgroundSprite;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            GameFieldSprites gameFieldSprites =
                ServicesProvider.GameSettingsService.SelectedGameTheme.GameSprites;
            var sprite = gameFieldSprites.BackgroundSprite;
            if (sprite == null)
                _backgroundSprite.enabled = false;
            else
            {
                _backgroundSprite.enabled = true;
                _backgroundSprite.sprite = sprite;
            }
        }
    }
}