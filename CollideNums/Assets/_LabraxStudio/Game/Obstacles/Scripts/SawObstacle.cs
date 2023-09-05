using System.Collections.Generic;
using LabraxStudio.App.Services;
using UnityEngine;
using LabraxStudio.Meta.GameField;
using LabraxStudio.Sound;
using LabraxStudio.UiAnimator;
using Sirenix.OdinInspector;

namespace LabraxStudio.Game.Obstacles
{
    public class SawObstacle : AObstacle
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private SpriteRenderer _sawMain;

        [SerializeField]
        private SpriteRenderer _sawPin;

        [SerializeField]
        private SpriteRenderer _sawShadow;

        [SerializeField]
        private UIAnimator _sawAnimator;

        [SerializeField]
        private List<ParticleSystem> _particleSystems;

        [Title("Layers settings")]
        [SerializeField]
        private int _shadowBaseLayer = 2;

        [SerializeField]
        private int _mainBaseLayer = 3;

        [SerializeField]
        private int _shadowAnimationLayer = 4;

        [SerializeField]
        private int _mainAnimationLayer = 5;

        // FIELDS: -------------------------------------------------------------------

        private List<Color> _colors = new List<Color>() {Color.white};

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        private void OnDestroy()
        {
            CancelInvoke(nameof(SwitchOffAnimation));
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void SetupObstacle(ObstacleType obstacleType, Vector2Int cell,
            ObstaclesSprites obstaclesSprites, Direction obstacleDirection)
        {
            _cell = cell;
            _obstacleType = obstacleType;
            SetupSprites(obstaclesSprites);
            _sawAnimator.Play();
        }

        [Button]
        public override void PlayTileCollideEffect()
        {
            var currentTile = ServicesProvider.GameFlowService.TilesController.GetTrackedTile();
            if (currentTile == null || currentTile.Tile == null)
                return;

            GameSoundMediator.Instance.PlayTileCollideSawSFX();
            Color particlesColor = GetColor(currentTile.Tile.Value);

            foreach (var system in _particleSystems)
            {
                var mainModule = system.main;
                mainModule.startColor = particlesColor;
                system.Play();
            }

            CancelInvoke(nameof(SwitchOffAnimation));
            _sawShadow.sortingOrder = _shadowAnimationLayer;
            _sawMain.sortingOrder = _mainAnimationLayer;
            Invoke(nameof(SwitchOffAnimation), 0.35f);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetupSprites(ObstaclesSprites obstaclesSprites)
        {
            _sawMain.sprite = obstaclesSprites.SawMain;
            _sawPin.sprite = obstaclesSprites.SawPin;
            _sawShadow.sprite = obstaclesSprites.SawShadow;
            _colors = obstaclesSprites.ParticalsColors;
            if (_colors.Count == 0)
                _colors.Add(Color.white);
        }

        private Color GetColor(int tile)
        {
            if (tile >= _colors.Count)
                return _colors[0];

            return _colors[tile];
        }

        private void SwitchOffAnimation()
        {
            _sawShadow.sortingOrder = _shadowBaseLayer;
            _sawMain.sortingOrder = _mainBaseLayer;
        }
    }
}