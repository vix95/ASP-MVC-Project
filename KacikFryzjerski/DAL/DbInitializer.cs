using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KacikFryzjerski.Models;
using KacikFryzjerski.Migrations;
using System.Data.Entity.Migrations;

namespace KacikFryzjerski.DAL
{
    public class DbInitializer : MigrateDatabaseToLatestVersion<DbContext, Configuration>
    {
        public static void SeedDbData(DbContext context)
        {
            var categories = new List<CategoryModels>
            {
                new CategoryModels() { Id = 1, Category_name = "Lakiery" },
                new CategoryModels() { Id = 2, Category_name = "Nożyczki" },
                new CategoryModels() { Id = 3, Category_name = "Grzebienie" }
            };

            categories.ForEach(k => context.Categories.AddOrUpdate(k));
            context.SaveChanges();

            var products = new List<ProductModels>
            {
                new ProductModels() { 
                    Id = 1,
                    Product_category_id = 1, 
                    Product_name = "Trwały lakier",
                    Product_description = "Lakier zapewniający trwałą fryzurę na kilka godzin",
                    Product_price = 12.99,
                    Product_image_path = "trwaly_lakier.jpg",
                    Product_created_at = DateTime.Now},

                new ProductModels() {
                    Id = 2,
                    Product_category_id = 1,
                    Product_name = "Pół trwały lakier",
                    Product_description = "Pół trwały lakier zapewniający pół trwałą fryzurę na kilka godzin",
                    Product_price = 10.99,
                    Product_image_path = "trwaly_lakier.jpg",
                    Product_created_at = DateTime.Now},

                new ProductModels() {
                    Id = 3,
                    Product_category_id = 2,
                    Product_name = "Profesjonalne nożyczki",
                    Product_description = "Profesjonalne nożyczki zapewnią Tobie profesjonalne cięcie.",
                    Product_price = 59.89,
                    Product_image_path = "trwaly_lakier.jpg",
                    Product_created_at = DateTime.Now},
            };

            products.ForEach(k => context.Products.AddOrUpdate(k));
            context.SaveChanges();
        }
    }
}