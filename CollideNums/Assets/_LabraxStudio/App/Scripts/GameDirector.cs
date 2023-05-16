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

            if (ServicesFabric.GameSettingsService.GetGameSettings() == null)
                throw new Exception("Не назначен файл настроек в starter");

            SetupManagers();
            Debug.unityLogger.logEnabled = ServicesFabric.GameSettingsService.GetGlobalSettings().UsesUnityLogs;
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
            var gameSettings = ServicesFabric.GameSettingsService.GetGameSettings();
            PlayerDataService.Initialize();
            LevelDataService.Initialize();
            LevelMetaService.Initialize(gameSettings.LevelsList);
            GameManager.Instance.Initialize();

            if (!gameSettings.LaunchSettings.EnableTutorial)
                PlayerDataService.SetTutorialState(true);

            ScreenManager.Instance.Initialize();
            SoundManager.Instance.Setup();

            if (PlayerDataService.IsFirstStart)
            {
                PlayerDataService.SetFirstStartState(false);
                ServicesFabric.GameDataService.SaveGameData();
            }

            GameManager.Instance.SaveTime();
        }
    }
}