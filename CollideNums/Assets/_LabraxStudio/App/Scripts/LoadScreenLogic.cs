using System.Collections;
using LabraxStudio.Base;
using LabraxStudio.Managers;
using LabraxStudio.UI;
using UnityEngine;

namespace LabraxStudio.App
{
    public class LoadScreenLogic : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private LoadingUIE _loading;

        [SerializeField]
        private float _firstLoadTime = 5f;

        // FIELDS: --------------------------------------------------------------------------------

        private bool _isFirstStart;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake() =>
            _isFirstStart = ServicesFabric.GameDataService.GetGameData().IsFirstStart;

        private void Start()
        {
#if !UNITY_EDITOR
            LoadGameDirectorAsynchronously();
            //SupersonicWisdom.Api.AddOnReadyListener(OnSupersonicWisdomReady);
            //SupersonicWisdom.Api.Initialize();
#else
            Utils.PerformWithDelay(this, 0.1f, SelectScene);
#endif
        }

        private void OnSupersonicWisdomReady()
        {
            StartCoroutine(LoadGameDirectorAsynchronously());
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SelectScene()
        {
            int levelsCount = LevelManager.LevelsCount;

            if (levelsCount <= 1)
            {
                _loading.LoadScene(Scenes.MainMenu);
                return;
            }

            bool loadMainMenuScene = LevelManager.GetLevelData(1).IsUnlocked;
            Scenes scene = loadMainMenuScene ? Scenes.MainMenu : Scenes.Game;
            _loading.LoadScene(scene);
        }

        private IEnumerator LoadGameDirectorAsynchronously()
        {
            if (_isFirstStart)
                yield return new WaitForSeconds(_firstLoadTime);
            else
                yield return new WaitForSeconds(1.55f);

            ResourceRequest resourceRequest = Resources.LoadAsync("Game Director");

            while (resourceRequest.progress < 1)
            {
                yield return new WaitForSeconds(0.01f);
            }

            //DontDestroyOnLoad(Instantiate(resourceRequest.asset));
            Object gameDirectorInstance = Instantiate(resourceRequest.asset);
            DontDestroyOnLoad(gameDirectorInstance);
            yield return new WaitForSeconds(0.15f);
            SelectScene();
        }
    }
}