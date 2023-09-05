using System;
using LabraxEditor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta.GameField
{
    [Serializable]
    public class GameTheme : ScriptableMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private string _themeID;

        [SerializeField]
        [PreviewField(ObjectFieldAlignment.Left, Height = 60)]
        private Sprite _themeIcon;

        [SerializeField]
        private GameFieldSprites _gameSprites;

        // PROPERTIES: ----------------------------------------------------------------------------

        public string ThemeId => _themeID;
        public Sprite ThemeIcon => _themeIcon;
        public GameFieldSprites GameSprites => _gameSprites;
    }
}