using System.Collections.Generic;
using System.Threading.Tasks;
using LabraxStudio.Meta.Levels;
using UnityEngine;

namespace LabraxStudio.Game.Obstacles
{
    public class ObstaclesController : MonoBehaviour
    {
         // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private ObstaclesGenerator _obstaclesGenerator = new ObstaclesGenerator();

        // FIELDS: -------------------------------------------------------------------

        private List<AObstacle> _obstacles = new List<AObstacle>();

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            _obstaclesGenerator.Initialize();
        }

        public void GenerateObstacles(LevelMeta levelMeta)
        {
            _obstacles = _obstaclesGenerator.GenerateObstacles(levelMeta.Width, levelMeta.Height, levelMeta.ObstaclesMatrix);
        }
        
        public void ClearObstacles()
        {
            RemoveGameField(new List<AObstacle>(_obstacles));
            _obstacles.Clear();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async void RemoveGameField(List<AObstacle> obstacles)
        {
            foreach (var obstacle in obstacles)
            {
                obstacle.DestroySelf();
                await Task.Delay(10);
            }
        }

        private AObstacle GetObstacle(Vector2Int obstacleCell)
        {
            return _obstacles.Find(g => g.Cell == obstacleCell);
        }
    }
}
