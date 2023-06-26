#if UNITY_EDITOR
using Sirenix.Utilities;
#endif
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
#endif
    }
}