using System;
using LabraxStudio.App.Services;
using LabraxStudio.Sound;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LabraxStudio.App
{
    public class GameDirector : AppDirector
    {
        // FIELDS: --------------------------------------------------------------------------------
        public static GameDirector Instance { get; private set; }

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        protected override void Start()
        {
            base.Start();

            if (ServicesAccess.GameSettingsService.GetGameSettings() == null)
                throw new Exception("Не назначен файл настроек в starter");

            SetupManagers();
            Debug.unityLogger.logEnabled = ServicesAccess.GameSettingsService.GetGlobalSettings().UsesUnityLogs;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------
#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute()
        {
            Object gameDirectorPrefab = Resources.Load("Game Director");
            Object gameDirectorInstance = Instantiate(gameDirectorPrefab);
            DontDestroyOnLoad(gameDirectorInstance);
        }
#endif

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetupManagers()
        {
            var gameSettings = ServicesAccess.GameSettingsService.GetGameSettings();
            ServicesAccess.PlayerDataService.Initialize();
            ServicesAccess.LevelDataService.Initialize();
            ServicesAccess.LevelMetaService.Initialize(gameSettings.LevelsList);
            GameManager.Instance.Initialize();

            if (!gameSettings.LaunchSettings.EnableTutorial)
                ServicesAccess.PlayerDataService.SetTutorialState(true);

            ScreenManager.Instance.Initialize();
            SoundManager.Instance.Setup();

            if (ServicesAccess.PlayerDataService.IsFirstStart)
            {
                ServicesAccess.PlayerDataService.SetFirstStartState(false);
                ServicesAccess.GameDataService.SaveGameData();
            }

            GameManager.Instance.SaveTime();
        }
    }
}