using LabraxStudio.Managers;
using LabraxStudio.Meta;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Sound
{
    public class GameSoundMediator : SharedManager<GameSoundMediator>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _levelStartSoundSFX;

        // FIELDS: --------------------------------------------------------------------------------

        private SoundManager _soundManager;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            _soundManager = SoundManager.Instance;
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        [Button, DisableInEditorMode]
        public void PlayLevelStartSound() => _soundManager.PlaySound(_levelStartSoundSFX);
    }
}