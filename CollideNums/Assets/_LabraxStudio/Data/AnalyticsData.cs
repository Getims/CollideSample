using System;
using UnityEngine;

namespace LabraxStudio.Data
{
    [Serializable]
    public class AnalyticsData
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private bool _isFirstGameStart = true;

        [SerializeField]
        private int _sessionNumber = 0;

        [SerializeField]
        private int _sessionTime = 0;
        
        [SerializeField]
        private bool _wasFirstPurchase = false;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsFirstGameStart => _isFirstGameStart;
        public int SessionNumber => _sessionNumber;
        public int SessionTime => _sessionTime;
        public bool WasFirstPurchase => _wasFirstPurchase;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetFirstGameStart(bool isFirstGameStart) => _isFirstGameStart = isFirstGameStart;
        public void SetSessionNumber(int number) => _sessionNumber = number;
        public void SetSessionTime(int time) => _sessionTime = time;
        public bool SetFirstPurchaseState(bool wasFirstPurchase) => _wasFirstPurchase = wasFirstPurchase;
    }
}