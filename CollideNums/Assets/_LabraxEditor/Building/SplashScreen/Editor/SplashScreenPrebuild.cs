using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace LabraxStudio.Building
{
    public class SplashScreenPrebuild : IPreprocessBuildWithReport
    {
        public int callbackOrder => 1;

        public static void GenerateSplashScreen()
        {
            PlayerSettings.SplashScreen.show = true;
            PlayerSettings.SplashScreen.animationMode = PlayerSettings.SplashScreen.AnimationMode.Static;
            PlayerSettings.SplashScreen.showUnityLogo = false;
            PlayerSettings.SplashScreen.backgroundColor = Color.black;
            PlayerSettings.SplashScreen.overlayOpacity = 0;
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            GenerateSplashScreen();
        }
    }
}