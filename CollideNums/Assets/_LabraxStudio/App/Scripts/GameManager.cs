using LabraxStudio.App.Services;
using LabraxStudio.Base;
using LabraxStudio.Loadscreen;
using LabraxStudio.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LabraxStudio.App
{
    public class GameManager : SharedManager<GameManager>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title("References")]
        [SerializeField]
        private LoadingUIE _loadingUIE;

        // FIELDS: -------------------------------------------------------------------

        private int _sessionStartTime;
        private static GameManager _gameManager;
        public static LoadingUIE LoadingUIE;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy() =>
            CancelInvoke(nameof(SaveTime));

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            _sessionStartTime = UnixTime.Now;
            LoadingUIE = _loadingUIE;
        }

        public void SaveTime()
        {
            /*
            CancelInvoke(nameof(SaveTime));
            int time = UnixTime.Now - _sessionStartTime;
            */
            Invoke(nameof(SaveTime), 7);
        }

        public static void LoadScene(Scenes scene)
        {
            if (LoadingUIE != null)
                LoadingUIE.LoadScene(scene);
            else
                SceneManager.LoadScene(scene.ToString());
        }

        public static void LoadScene(string scene)
        {
            if (LoadingUIE != null)
                LoadingUIE.LoadScene(scene);
            else
                SceneManager.LoadScene(scene);
        }

        public static void ReloadScene()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}