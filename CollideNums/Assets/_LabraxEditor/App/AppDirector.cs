using UnityEngine;
using LabraxStudio.Managers;

namespace LabraxStudio.App
{
    public class AppDirector : MonoBehaviour
    {
        private SharedManager[] _managers;
        private bool _isDirectorReady;

        private const string LOG_KEY = "AppDirector";
      
        protected bool IsDirectorReady => _isDirectorReady;
        protected static AppDirector _instance;
        protected int[] _qualitySettingsNames;

        public static string GetApplicationVersion()
        {
            return Application.version;
        }

        protected virtual void Awake()
        {
#if SK_LOG
            Debug.unityLogger.logEnabled = true;
#else
            Debug.unityLogger.logEnabled = true;
#endif
        }

        protected virtual void Start()
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            
            FramerateManager.SetTargetFramerate(true);
            Application.backgroundLoadingPriority = ThreadPriority.Low;

            _managers = GetComponentsInChildren<SharedManager>();

            foreach (SharedManager _mgr in _managers)
            {
                _mgr.InitManager();
            }

            int namesCount = QualitySettings.names.Length;
            _qualitySettingsNames = new int[namesCount];
            for (int i = 0; i < namesCount; i++)
                _qualitySettingsNames[i] = QualitySettings.names[i].GetHashCode();

            _isDirectorReady = true;
        }

        protected virtual void OnEnable()
        {
            Application.logMessageReceived += OnLogReceived;
        }

        protected virtual void OnDisable()
        {
            Application.logMessageReceived -= OnLogReceived;
        }

        protected virtual void OnDestroy()
        {
        }

        protected virtual void OnApplicationPause(bool _pause)
        {
            if (_isDirectorReady)
            {
                if (_pause)
                {
                    NotifyManagersAboutDeactivate();
                }

                PlayerPrefs.Save();
            }
        }

        protected virtual void OnApplicationQuit()
        {
            if (_isDirectorReady)
            {
                NotifyManagersAboutDeactivate();
                PlayerPrefs.Save();
            }
        }

        protected virtual void OnLogReceived(string _logEntry, string _stackTrace, LogType _logType)
        {
            if (_logType == LogType.Exception)
            {
                ShutdownApp();
            }
        }

        private void NotifyManagersAboutDeactivate()
        {
            foreach (SharedManager _mgr in _managers)
            {
                _mgr.OnAppDeactivate();
            }
        }

        private void ShutdownApp()
        {

#if UNITY_EDITOR
            Debug.Break();
#else
            Application.Quit();
#endif
        }

        protected void SetQuality(int qualityHashCode)
        {
            int _qualityCount = _qualitySettingsNames.Length;

            for (int i = 0; i < _qualityCount; i++)
            {
                if (_qualitySettingsNames[i] == qualityHashCode)
                {
                    QualitySettings.SetQualityLevel(i);
                    break;
                }
            }
        }

    }
}