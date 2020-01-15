using KacikFryzjerski.DAL;
using KacikFryzjerski.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.Infrastructure
{
    public class ProductDetailsDynamicNodeProvider : DynamicNodeProviderBase
    {
        private DbContext db = new DbContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var returnValue = new List<DynamicNode>();

            foreach (ProductModels product in db.Products)
            {
                DynamicNode dynamicNode = new DynamicNode();
                dynamicNode.Title = product.Product_name;
                dynamicNode.Key = "product_" + product.Id;
                dynamicNode.ParentKey = "category_" + product.Product_category_id;
                dynamicNode.RouteValues.Add("id", product.Id);

                returnValue.Add(dynamicNode);
            }

            return returnValue;
        }
    }
}