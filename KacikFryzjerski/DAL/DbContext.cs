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

        public DbSet<ItemModels> items { get; set; }
        public DbSet<CategoryModels> categories { get; set; }
        public DbSet<OrderModels> orders { get; set; }
        public DbSet<OrderItemModels> orderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<DbContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}