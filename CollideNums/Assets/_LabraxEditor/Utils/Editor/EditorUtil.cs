using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace LabraxStudio.Base
{
    [InitializeOnLoad]
    public class EditorUtil
    {
        [MenuItem("📄 Game Data/🚫 Clear Data")]
        public static void ClearData()
        {
            PlayerPrefs.DeleteAll();
            FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
        }

        [MenuItem("🕹 Labrax Studio/✈ Scenes/🗺 Main Menu")]
        public static void OpenMainMenuScene() =>
            OpenScene("Assets/_LabraxStudio/Scenes/PuzzleMap.unity");

        [MenuItem("🕹 Labrax Studio/✈ Scenes/🚀 Game")]
        public static void OpenGameScene() =>
            OpenScene("Assets/_LabraxStudio/Scenes/Game.unity");

        [MenuItem("🕹 Labrax Studio/✈ Scenes/⚡ Bootstrap")]
        public static void OpenBootstrapScene() =>
            OpenScene("Assets/_LabraxStudio/Scenes/Bootstrap.unity");

        [MenuItem("🕹 Labrax Studio/⚙ Labrax Editor")]
        private static void OpenWindow() =>
            LabraxEditor.LabraxEditor.OpenWindow();

        [MenuItem("🕹 Labrax Studio/🎮 Run Game")]
        public static void RunGame()
        {
            if (EditorApplication.isPlaying)
                return;

            OpenScene("Assets/_LabraxStudio/Scenes/Bootstrap.unity");
            EditorApplication.isPlaying = true;
        }

        private static void OpenScene(string path)
        {
            if (!Application.isPlaying && EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
        }
    }
}