using System.Collections.Generic;
using UnityEngine;

namespace LabraxStudio.AnalyticsIntegration.IAP
{
   internal static class PurchaseRestoreListener
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public static List<ProductName> ProductsNames => _productsNames;
       
        // FIELDS: -------------------------------------------------------------------

        [SerializeField]
        private static List<ProductName> _productsNames = new List<ProductName>();
        
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public static void Initialize()
        {
            _productsNames = new List<ProductName>();
        }

        public static void AddRestoredProduct(ProductName productName)
        {
            _productsNames.Add(productName);
        }
    }
}