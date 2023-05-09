using LabraxStudio.Managers;
using LabraxStudio.Meta;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Sound
{
    public class GlobalSoundMediator : SharedManager<GlobalSoundMediator>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _uiClickSFX;

        // FIELDS: --------------------------------------------------------------------------------

        private SoundManager _soundManager;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() =>
            _soundManager = SoundManager.Instance;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        [Title("Buttons")]
        [Button, DisableInEditorMode]
        public void PlayUIClick() => _soundManager.PlaySound(_uiClickSFX);
    }
}