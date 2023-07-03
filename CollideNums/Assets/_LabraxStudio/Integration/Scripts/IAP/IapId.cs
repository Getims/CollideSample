using System;
using UnityEngine;

namespace LabraxStudio.AnalyticsIntegration.IAP
{
    [Serializable]
    public class IapId
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField]
        [HideInInspector]
        public string Id;

        [SerializeField]
        public ProductName Product;

        [SerializeField]
        [Min(0)]
        public int CashPoints = 0; 

        // PROPERTIES: ----------------------------------------------------------------------------

        private string ElementName => $"'Enum: {Product}'";
    }

    public enum ProductName
    {
        NoAds = 0,
        NoProduct = 1
    }
}