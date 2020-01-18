using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KacikFryzjerski.Models;
using KacikFryzjerski.Migrations;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
                    Product_created_at = DateTime.Now,
                    Product_is_bestseller = true},

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
                    Product_image_path = "nozyczki.jpg",
                    Product_created_at = DateTime.Now,
                    Product_is_bestseller = true},

                new ProductModels() {
                    Id = 4,
                    Product_category_id = 3,
                    Product_name = "Grzebień profesjonalny",
                    Product_description = "Do rozczesania nawet najbardziej trudnych włosów.",
                    Product_price = 12.89,
                    Product_image_path = "grzebien.jpg",
                    Product_created_at = DateTime.Now,
                    Product_is_bestseller = true},
            };

            products.ForEach(k => context.Products.AddOrUpdate(k));
            context.SaveChanges();
        }

        public static void SeedUsers(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            const string name = "admin@itvix.pl";
            const string password = "Admin1!";
            const string roleName = "Admin";

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
            }

            // create Admin role if doesn't exists
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            // add user to Admin role if doensn't exists
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}