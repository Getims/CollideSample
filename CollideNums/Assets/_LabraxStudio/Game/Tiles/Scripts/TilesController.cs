using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class TilesController : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private TilesGenerator _tilesGenerator = new TilesGenerator();

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(LevelMeta levelMeta)
        {
            _tilesGenerator.Initialize();
            _tilesGenerator.GenerateTiles(levelMeta.Width, levelMeta.Height, levelMeta.TilesMatrix);
        }
    }
}