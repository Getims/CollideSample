#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LabraxStudio.Editor
{
    public static class MatrixHelper
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public static Dictionary<int, Texture> TexturesDictionary { get; private set; } = new Dictionary<int, Texture>();
        public static Dictionary<int, Texture> TilesDictionary { get; private set; } = new Dictionary<int, Texture>();
        public static int SpritesCount => TexturesDictionary.Count;

        // FIELDS: --------------------------------------------------------------------------------

        private const string SpritesPath = "Assets/_LabraxStudio/Meta/ForEditor/Levels/Sprites/";

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
            LoadTopTypes(ref idCount);
            LoadBottomTypes(ref idCount);
            LoadLeftTypes(ref idCount);
            LoadRightTypes(ref idCount);
            LoadGatesTypes(ref idCount);
            LoadTiles();
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

        private static void LoadLeftTypes(ref int idCount)
        {
            AddKeyAndValue(idCount, "Left");
            idCount++;
            AddKeyAndValue(idCount, "Left_bottom");
            idCount++;
            AddKeyAndValue(idCount, "Left_top");
            idCount++;
        }

        private static void LoadTopTypes(ref int idCount)
        {
            AddKeyAndValue(idCount, "Top");
            idCount++;
        }

        private static void LoadRightTypes(ref int idCount)
        {
            AddKeyAndValue(idCount, "Right");
            idCount++;
            AddKeyAndValue(idCount, "Right_bottom");
            idCount++;
            AddKeyAndValue(idCount, "Right_top");
            idCount++;
        }

        private static void LoadBottomTypes(ref int idCount)
        {
            AddKeyAndValue(idCount, "Bottom");
            idCount++;
        }

        private static void LoadGatesTypes(ref int idCount)
        {
            AddKeyAndValue(idCount, "Gate_2");
            idCount++;
            AddKeyAndValue(idCount, "Gate_4");
            idCount++;
            AddKeyAndValue(idCount, "Gate_8");
            idCount++;
            AddKeyAndValue(idCount, "Gate_16");
            idCount++;
            AddKeyAndValue(idCount, "Gate_32");
            idCount++;
            AddKeyAndValue(idCount, "Gate_64");
            idCount++;
        }
        
        private static void LoadTiles()
        {
            for (int i = 0; i < 6; i++)
            {
                AddKeyAndValue(i, "Tile_" + i, true);
            }
        }

        private static void AddKeyAndValue(int key, string textureName, bool isTile = false)
        {
            Texture texture = LoadTexture(textureName);

            if (isTile)
                TilesDictionary.Add(key, texture);
            else
                TexturesDictionary.Add(key, texture);
        }

        private static Texture LoadTexture(string fileName)
        {
            var jpg = ".jpg";
            var png = ".png";
            var texture = EditorGUIUtility.Load(SpritesPath + fileName + jpg) as Texture2D;

            if (texture == null)
                texture = EditorGUIUtility.Load(SpritesPath + fileName + png) as Texture2D;

            return texture;
        }
    }
}
#endif