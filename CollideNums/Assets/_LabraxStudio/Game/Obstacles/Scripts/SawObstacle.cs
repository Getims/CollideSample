using System;
using System.Collections.Generic;
using LabraxStudio.App.Services;
using UnityEngine;
using LabraxStudio.Meta.GameField;
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

        // FIELDS: -------------------------------------------------------------------

        private List<Color> _colors = new List<Color>() {Color.white};

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

            Color particlesColor = GetColor(currentTile.Tile.Value);

            foreach (var system in _particleSystems)
            {
                var mainModule = system.main;
                mainModule.startColor = particlesColor;
                system.Play();
            }
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
    }
}