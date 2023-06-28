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
        private SFXMeta _tileCollideGateSFX;

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _tileCollideSFX;

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _tileMoveSFX;

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _tileSuperMoveSFX;

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _tilesGatePassSFX;

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _tilesMergeSFX;
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
        public void PlayTileCollideGateSFX() => _soundManager.PlaySound(_tileCollideGateSFX);

        [Button, DisableInEditorMode]
        public void PlayTileCollideSFX() => _soundManager.PlaySound(_tileCollideSFX);

        [Button, DisableInEditorMode]
        public void PlayTileMoveSFX() => _soundManager.PlaySound(_tileMoveSFX);

        [Button, DisableInEditorMode]
        public void PlayTileSuperMoveSFX() => _soundManager.PlaySound(_tileSuperMoveSFX);

        [Button, DisableInEditorMode]
        public void PlayTilesGatePassSFX() => _soundManager.PlaySound(_tilesGatePassSFX);

        [Button, DisableInEditorMode]
        public void PlayTilesMergeSFX() => _soundManager.PlaySound(_tilesMergeSFX);
    }
}