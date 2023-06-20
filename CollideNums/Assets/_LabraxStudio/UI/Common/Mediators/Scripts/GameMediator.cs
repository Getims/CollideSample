using LabraxStudio.Managers;
using LabraxStudio.UI.Common;
using LabraxStudio.UI.Common.IconsSpawner;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UI.GameScene
{
    public class GameMediator : SharedManager<GameMediator>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(References)]
        [SerializeField, Required, SceneObjectsOnly] private IconsFlyAnimationHandler _coinsFlyAnimation;
        
        // FIELDS: --------------------------------------------------------------------------------
        
        private const string References = "References";
        private const string Buttons = "Buttons";

        // PUBLIC METHODS: ------------------------------------------------------------------------

        [Title(Buttons)]
        [Button, DisableInEditorMode] public void StartCoinsFlyAnimation() => _coinsFlyAnimation.StartAnimation();
        [Button, DisableInEditorMode] public void StartCoinsFlyAnimation(Transform startPosition) => _coinsFlyAnimation.StartAnimation(startPosition);

    }
}