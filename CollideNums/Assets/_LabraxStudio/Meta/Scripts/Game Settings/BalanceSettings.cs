using System;
using UnityEngine;

namespace LabraxStudio.Meta
{
    [Serializable]
    public class BalanceSettings
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Min(0)]
        [Tooltip("Начальное количество денег.")]
        private int _startMoney;

        // PROPERTIES: ----------------------------------------------------------------------------
        public int StartMoney => _startMoney;
    }
}