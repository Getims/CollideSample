using TMPro;
using UnityEngine;

namespace LabraxStudio.UI
{
    public class LoadCounter : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private TextMeshProUGUI _counter;

        // FIELDS: -------------------------------------------------------------------

        private const string _loadStr = "Load time: ";
        private const string _startStr = "Loading... ";

        private static bool _isSetuped = false;
        private static TextMeshProUGUI _staticCounter;
        private static float _startTime = 0;

        
        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            _staticCounter = _counter;
            _isSetuped = true;
            DontDestroyOnLoad(this);
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------
        
        public static void SetStartLoad()
        {
            if(!_isSetuped)
                return;
            
            _staticCounter.text = _startStr;
            _startTime = Time.realtimeSinceStartup;
        }

        public static void SetLoadComplete()
        {
            if(!_isSetuped)
                return;

            var completeTime = Time.realtimeSinceStartup;
            var loadTime = completeTime - _startTime;
            _staticCounter.text = string.Format("{0}{1}", _loadStr, loadTime.ToString("n3"));
        }
    }
}
