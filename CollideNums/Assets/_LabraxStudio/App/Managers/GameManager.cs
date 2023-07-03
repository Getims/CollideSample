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

        public static LoadingUIE LoadingUIE;
        
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            LoadingUIE = _loadingUIE;
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