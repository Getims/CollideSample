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
        private SFXMeta _taskGatePassSFX;

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _tilesMergeSFX;

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _tileSplitBoosterSFX;

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _tileMultiplyBoosterSFX;

        [Title("Obstacles")]
        [SerializeField, Required, AssetsOnly]
        private SFXMeta _tileCollidePusherSFX;

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _tileCollideSawSFX;

        [SerializeField, Required, AssetsOnly]
        private SFXMeta _tileCollideHoleSFX;

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
        public void PlayTaskGatePassSFX() => _soundManager.PlaySound(_taskGatePassSFX);

        [Button, DisableInEditorMode]
        public void PlayTilesMergeSFX() => _soundManager.PlaySound(_tilesMergeSFX);

        [Button, DisableInEditorMode]
        public void PlayTileSplitByBoosterSFX() => _soundManager.PlaySound(_tileSplitBoosterSFX);

        [Button, DisableInEditorMode]
        public void PlayTileMultiplyByBoosterSFX() => _soundManager.PlaySound(_tileMultiplyBoosterSFX);

        [Button, DisableInEditorMode]
        public void PlayTileCollidePusherSFX() => _soundManager.PlaySound(_tileCollidePusherSFX);

        [Button, DisableInEditorMode]
        public void PlayTileCollideSawSFX() => _soundManager.PlaySound(_tileCollideSawSFX);

        [Button, DisableInEditorMode]
        public void PlayTileCollideHoleSFX() => _soundManager.PlaySound(_tileCollideHoleSFX);
    }
}