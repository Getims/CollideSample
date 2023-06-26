#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LabraxStudio.Editor
{
    public static class MatrixHelper
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public static Dictionary<int, Texture> TexturesDictionary { get; private set; } =
            new Dictionary<int, Texture>();

        public static int SpritesCount => TexturesDictionary.Count;

        // FIELDS: --------------------------------------------------------------------------------

        private const string SpritesPath = "Assets/_LabraxStudio/Meta/ForEditor/Levels/Sprites/";
        private const string GatesPath = "Assets/_LabraxStudio/Meta/ForEditor/Levels/Sprites/Gates/";
        private const string TilesPath = "Assets/_LabraxStudio/Meta/ForEditor/Levels/Sprites/Tiles/";

        private static bool _isInitialized;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void CheckInitialization()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;
            Initialize();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static void Initialize()
        {
            TexturesDictionary = new Dictionary<int, Texture>();

            int idCount = 0;
            LoadLockedTypes(ref idCount);
            LoadUnlockedTypes(ref idCount);
            LoadGatesTypes(ref idCount);
            LoadTiles(ref idCount);
        }

        private static void LoadLockedTypes(ref int idCount)
        {
            AddKeyAndValue(idCount, "Locked");
            idCount++;
        }

        private static void LoadUnlockedTypes(ref int idCount)
        {
            AddKeyAndValue(idCount, "Unlocked");
            idCount++;
        }

        private static void LoadGatesTypes(ref int idCount)
        {
            for (int i = 0; i < 16; i++)
            {
                AddKeyAndValue(idCount, "Gate_" + i, GatesPath);
                idCount++;
            }
        }

        private static void LoadTiles(ref int idCount)
        {
            for (int i = 0; i < 16; i++)
            {
                AddKeyAndValue(idCount, "Tile_" + i, TilesPath);
                idCount++;
            }
        }

        private static void AddKeyAndValue(int key, string textureName, string customPath = "")
        {
            Texture texture = LoadTexture(textureName, customPath);
            TexturesDictionary.Add(key, texture);
        }

        private static Texture LoadTexture(string fileName, string customPath = "")
        {
            string path = customPath == "" ? SpritesPath : customPath;

            var jpg = ".jpg";
            var png = ".png";
            var texture = EditorGUIUtility.Load(path + fileName + jpg) as Texture2D;

            if (texture == null)
                texture = EditorGUIUtility.Load(path + fileName + png) as Texture2D;

            return texture;
        }
    }
}
#endif