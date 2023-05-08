using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace LabraxStudio.App
{
    [CreateAssetMenu(fileName = "Atlases Meta", menuName = "🕹 Labrax Studio/Meta/Atlases Meta")]
    public class AtlasesMeta : ScriptableObject
    {
        [SerializeField]
        List<SpriteAtlas> hdAtlases;
        [SerializeField]
        List<SpriteAtlas> sdAtlases;

        List<SpriteAtlas> currentAtlases;

        public void SetupAtlases(bool _isHd)
        {
            if (_isHd)
                currentAtlases = hdAtlases;
            else
                currentAtlases = sdAtlases;
        }

        public SpriteAtlas GetAtlas(string _tag)
        {
            var _atlas = currentAtlases.Find(_a => _a.tag == _tag);
            return _atlas;
        }


        public List<SpriteAtlas> HdAtlases => hdAtlases;
        public List<SpriteAtlas> SdAtlases => sdAtlases;

    }
}
