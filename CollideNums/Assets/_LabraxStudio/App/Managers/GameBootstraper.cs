using LabraxStudio.Game;
using LabraxStudio.UI;
using UnityEngine;

namespace LabraxStudio.App
{

    public class GameBootstraper : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] 
        private GameFlowManager _gameFlowManager;
       
        [SerializeField] 
        private UIManager _uiManager;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        private void Start()
        {
            _gameFlowManager.Initialize();
            _uiManager.InitializeMenuUI();
            _uiManager.InitializeDebugMenu();
        }
    }
}
