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

        public static int DrawLevelEnumElement(Rect rect, int value, bool brushMode, bool isFieldMode, int brushSize,
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
                    value = GetLeftValue(brushMode, brushSize, value, isFieldMode);
                else
                    value = GetRightValue(brushMode, rightClickSize, value, isFieldMode);

                GUI.changed = true;
                Event.current.Use();
            }

            if (value == 0)
                UnityEditor.EditorGUI.DrawRect(rect.Padding(1), Color.black);
            else
            {
                Texture texture = MatrixHelper.TexturesDictionary[value - 1];
                UnityEditor.EditorGUI.DrawPreviewTexture(rect.Padding(1), texture);
            }
#endif

            return value;
        }

        private static int GetLeftValue(bool brushMode, int brushSize, int value, bool isFieldMode)
        {
#if !UNITY_EDITOR
            return value;
#else

            if (brushMode)
                return Mathf.Min(brushSize + 1, ColorsCount);

            int newValue = value + 1;

            if (isFieldMode)
            {
                if (newValue > 18)
                    newValue = 1;
                return newValue;
            }

            if (newValue == 1 || newValue == 2)
                return newValue;
            if (newValue == 3)
                return 19;
            if (newValue < 19 || newValue > ColorsCount)
                return 1;

            return newValue;
#endif
        }

        private static int GetRightValue(bool brushMode, int rightClickSize, int value, bool isFieldMode)
        {
#if !UNITY_EDITOR
            return value;
#else
            if (brushMode)
                return Mathf.Min(rightClickSize + 1, ColorsCount);

            int newValue = value - 1;

            if (isFieldMode)
            {
                if (newValue < 1)
                    newValue = 18;
                if (value == 19)
                    newValue = 1;
                if (newValue >= 19)
                    newValue = 1;
                return newValue;
            }

            if (newValue < 1)
                return ColorsCount;
            if (newValue == 18)
                return 2;
            if (newValue > 2 && newValue < 18)
                return 1;

            return newValue;
#endif
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