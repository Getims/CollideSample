using LabraxEditor;
using LabraxStudio.Game;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta.Levels
{
    public class BoosterMeta : ScriptableMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private BoosterType _boosterType;
        
        [SerializeField, Required]
        private Sprite _iconSprite;

        [SerializeField, LabelText("Receive For RV?")]
        private bool _receiveForRv;

        [SerializeField, Min(0), LabelText("Money Price")]
        [HideIf(nameof(_receiveForRv))]
        private int _price;

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool ReceiveForRv => _receiveForRv;
        public int MoneyPrice => _price;
        public Sprite IconSprite => _iconSprite;
        public BoosterType BoosterType => _boosterType;
    }
}