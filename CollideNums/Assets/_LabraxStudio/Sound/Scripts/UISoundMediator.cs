using LabraxStudio.Managers;
using LabraxStudio.Meta;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Sound
{
    public class UISoundMediator : SharedManager<UISoundMediator>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _coinsFlySFX;

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _taskCompleteSFX;

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _taskFailSFX;

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _victoryMenuOpenedSFX;

        // FIELDS: --------------------------------------------------------------------------------

        private SoundManager _soundManager;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() =>
            _soundManager = SoundManager.Instance;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        [Button, DisableInEditorMode]
        public void PlayCoinsFlySFX() => _soundManager.PlaySound(_coinsFlySFX);

        [Button, DisableInEditorMode]
        public void PlayTaskCompleteSFX() => _soundManager.PlaySound(_taskCompleteSFX);

        [Button, DisableInEditorMode]
        public void PlayTaskFailSFX() => _soundManager.PlaySound(_taskFailSFX);

        [Button, DisableInEditorMode]
        public void PlayVictoryMenuOpenedSFX() => _soundManager.PlaySound(_victoryMenuOpenedSFX);
    }
}