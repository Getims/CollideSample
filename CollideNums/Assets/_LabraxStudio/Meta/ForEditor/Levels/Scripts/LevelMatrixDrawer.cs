#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
#endif
using System;
using UnityEngine;

namespace LabraxStudio.Editor
{
    public static class LevelMatrixDrawer
    {
#if UNITY_EDITOR
        public static int ColorsCount => MatrixHelper.SpritesCount;
#endif

        public static int DrawLevelEnumElement(Rect rect, int value, bool brushMode, int brushSize,
            int rightClickSize)
        {
#if UNITY_EDITOR

            MatrixHelper.CheckInitialization();

            Event currentEvent = Event.current;
            bool isClickSuccessful = IsClickSuccessful(currentEvent, rect);

            if (isClickSuccessful)
            {
                bool increaseHeight = currentEvent.button == 0;

                if (increaseHeight)
                {
                    if (brushMode)
                        value = Mathf.Min(brushSize + 1, ColorsCount);
                    else
                        value = Mathf.Min(value + 1, ColorsCount);
                }
                else
                {
                    if (brushMode)
                        value = Mathf.Max(rightClickSize, 0);
                    else
                        value = Mathf.Max(value - 1, 0);
                }

                GUI.changed = true;
                Event.current.Use();
            }

            if (value == 0)
            {
                UnityEditor.EditorGUI.DrawRect(rect.Padding(1), Color.black);
            }
            else
            {
                Texture texture = MatrixHelper.TexturesDictionary[value - 1];
                UnityEditor.EditorGUI.DrawPreviewTexture(rect.Padding(1), texture);
            }
#endif

            return value;
        }

        public static int DrawTilesEnumElement(Rect rect, int value, bool brushMode, int brushSize)
        {
#if UNITY_EDITOR

            MatrixHelper.CheckInitialization();

            Event currentEvent = Event.current;
            bool isClickSuccessful = IsClickSuccessful(currentEvent, rect);
            int maxCount = MatrixHelper.TilesDictionary.Count;
            
            if (isClickSuccessful)
            {
                bool increaseHeight = currentEvent.button == 0;

                if (increaseHeight)
                {
                    if (brushMode)
                        value = Mathf.Min(brushSize + 1, maxCount);
                    else
                        value = Mathf.Min(value + 1, maxCount);
                }
                else
                    value = currentEvent.control ? 0 : Mathf.Max(value - 1, 0);

                GUI.changed = true;
                Event.current.Use();
            }

            if (value == 0 || value > MatrixHelper.TilesDictionary.Count)
            {
                UnityEditor.EditorGUI.DrawRect(rect.Padding(1), Color.black);
            }
            else
            {
                Texture texture = MatrixHelper.TilesDictionary[value - 1];
                UnityEditor.EditorGUI.DrawPreviewTexture(rect.Padding(1), texture);
            }

#endif
            return value;
        }

        public static Texture GetBrushTexture(int index)
        {
            Texture texture = new Texture2D(0, 0);
#if UNITY_EDITOR
            if (index >= 0)
                texture = MatrixHelper.TexturesDictionary[index];
#endif
            return texture;
        }

        public static Texture GetTilesBrushTexture(int index)
        {
            Texture texture = new Texture2D(0, 0);
#if UNITY_EDITOR
            if (index >= 0)
                texture = MatrixHelper.TilesDictionary[index];
#endif
            return texture;
        }
        
#if UNITY_EDITOR
        private static bool IsClickSuccessful(Event currentEvent, Rect rect)
        {
            if (currentEvent.type != EventType.MouseDown)
                return false;

            if (currentEvent.button != 0 && currentEvent.button != 1)
                return false;

            if (!rect.Contains(currentEvent.mousePosition))
                return false;

            return true;
        }

        private static Color GetEditorPaletteColor(int colorIndex)
        {
            var colorPalette = GetColorEditorPalette();
            Color color = colorPalette.Colors[colorIndex];
            color.a = 1;

            return color;
        }

        private static ColorPalette GetColorEditorPalette() =>
            ColorPaletteManager.Instance.ColorPalettes[0];
#endif
    }
}