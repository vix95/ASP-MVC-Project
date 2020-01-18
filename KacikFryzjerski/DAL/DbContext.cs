using KacikFryzjerski.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.DAL
{
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext() : base("DbContext")
        {

        }

        static DbContext()
        {
            Database.SetInitializer<DbContext>(new DbInitializer());
        }

        public DbSet<ProductModels> Products { get; set; }
        public DbSet<CategoryModels> Categories { get; set; }
        public DbSet<OrderModels> Orders { get; set; }
        public DbSet<OrderPositionModels> OrderProducts { get; set; }
        public DbSet<AccountModels> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<DbContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}