using LabraxStudio.Base;
using LabraxStudio.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LabraxStudio.Managers
{
    public class GameManager : SharedManager<GameManager>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title("References")]
        [SerializeField]
        private LoadingUIE _loadingUIE;

        // PROPERTIES: ----------------------------------------------------------------------------

        public LoadingUIE LoadingUIE => _loadingUIE;
        
        // FIELDS: -------------------------------------------------------------------
        
        private Scenes _lastScene = Scenes.NULL;
        private int _sessionStartTime;
        private static GameManager _gameManager;
        private static bool _isLoadingUieNotNull;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        private void OnDestroy() =>
            CancelInvoke(nameof(SaveTime));

        // PUBLIC METHODS: -----------------------------------------------------------------------
       
        public void Initialize()
        {
            _sessionStartTime = UnixTime.Now;
            PlayerManager.Initialize();
            _isLoadingUieNotNull = _loadingUIE != null;
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
            if (_isLoadingUieNotNull)
                _gameManager.LoadingUIE.LoadScene(scene);
            else
                SceneManager.LoadScene(scene.ToString());
        }

        public static void LoadScene(string scene)
        {
            if (_gameManager.LoadingUIE != null)
                _gameManager.LoadingUIE.LoadScene(scene);
            else
                SceneManager.LoadScene(scene);
        }
        
        public static void ReloadScene()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}