using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.Infrastructure
{
    public class AppConfig
    {
        private static string _imagesCatalog = ConfigurationManager.AppSettings["Images"];

        public static string ImagesCatalog
        {
            get
            {
                return _imagesCatalog;
            }
        }

        private static string _storeImagesCatalog = ConfigurationManager.AppSettings["StoreImages"];

        public static string StoreImagesCatalog
        {
            get
            {
                return _storeImagesCatalog;
            }
        }
    }
}