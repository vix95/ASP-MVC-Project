using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KacikFryzjerski.Models;

namespace KacikFryzjerski.DAL
{
    public class DbInitializer : DropCreateDatabaseAlways<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            SeedDbData(context);
            base.Seed(context);
        }

        private void SeedDbData(DbContext context)
        {
            var categories = new List<CategoryModels>
            {
                new CategoryModels() { Id = 1, Category_name = "Lakiery" },
                new CategoryModels() { Id = 2, Category_name = "Nożyczki" },
                new CategoryModels() { Id = 3, Category_name = "Grzebienie" }
            };

            categories.ForEach(k => context.categories.Add(k));
            context.SaveChanges();

            var items = new List<ItemModels>
            {
                new ItemModels() { 
                    Id = 1,
                    Item_category_id = 1, 
                    Item_name = "Trwały lakier",
                    Item_description = "Lakier zapewniający trwałą fryzurę na kilka godzin",
                    Item_price = 12.99,
                    Item_image_path = "trwaly_lakier.jpg",
                    Item_created_at = DateTime.Now},

                new ItemModels() {
                    Id = 2,
                    Item_category_id = 1,
                    Item_name = "Pół trwały lakier",
                    Item_description = "Pół trwały lakier zapewniający pół trwałą fryzurę na kilka godzin",
                    Item_price = 10.99,
                    Item_image_path = "trwaly_lakier.jpg",
                    Item_created_at = DateTime.Now},

                new ItemModels() {
                    Id = 3,
                    Item_category_id = 2,
                    Item_name = "Profesjonalne nożyczki",
                    Item_description = "Profesjonalne nożyczki zapewnią Tobie profesjonalne cięcie.",
                    Item_price = 59.89,
                    Item_image_path = "trwaly_lakier.jpg",
                    Item_created_at = DateTime.Now},
            };

            items.ForEach(k => context.items.Add(k));
            context.SaveChanges();
        }
    }
}