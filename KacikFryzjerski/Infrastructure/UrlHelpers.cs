using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KacikFryzjerski.Infrastructure
{
    public static class UrlHelpers
    {
        public static string ImagePath(this UrlHelper helper, string imageName)
        {
            var catalog = AppConfig.ImagesCatalog;
            var catalog_path = Path.Combine(catalog, imageName);
            var return_catalog_path = helper.Content(catalog_path);

            return return_catalog_path;
        }
        public static string StoreItemImagePath(this UrlHelper helper, string itemName)
        {
            var catalog = AppConfig.StoreImagesCatalog;
            var catalog_path = Path.Combine(catalog, itemName);
            var return_catalog_path = helper.Content(catalog_path);

            return return_catalog_path;
        }
    }
}