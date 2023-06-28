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

        [SerializeField]
        private BoosterCost _boosterCost = BoosterCost.Free;
        
        [SerializeField, Min(0), LabelText("Money Price")]
        [ShowIf(nameof(RecieveForMoney))]
        private int _price;

        // PROPERTIES: ----------------------------------------------------------------------------

        public BoosterCost BoosterCost => _boosterCost;
        public int MoneyPrice => _price;
        public Sprite IconSprite => _iconSprite;
        public BoosterType BoosterType => _boosterType;

        private bool RecieveForMoney => _boosterCost == BoosterCost.Money;

    }
}