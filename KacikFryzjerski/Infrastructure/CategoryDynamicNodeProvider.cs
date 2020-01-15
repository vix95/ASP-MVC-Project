using KacikFryzjerski.DAL;
using KacikFryzjerski.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.Infrastructure
{
    public class CategoryDynamicNodeProvider : DynamicNodeProviderBase
    {
        private DbContext db = new DbContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var returnValue = new List<DynamicNode>();

            foreach (CategoryModels category in db.Categories)
            {
                DynamicNode dynamicNode = new DynamicNode();
                dynamicNode.Title = category.Category_name;
                dynamicNode.Key = "category_" + category.Id;
                dynamicNode.RouteValues.Add("category_id", category.Id);

                returnValue.Add(dynamicNode);
            }

            return returnValue;
        }
    }
}