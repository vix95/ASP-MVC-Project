using KacikFryzjerski.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.DAL
{
    public class ProjectDbContext : System.Data.Entity.DbContext
    {
        public ProjectDbContext() : base("ProjectDbContext")
        {

        }

        static ProjectDbContext()
        {
            Database.SetInitializer<ProjectDbContext>(new DbInitializer());
        }

        public static ProjectDbContext Create()
        {
            return new ProjectDbContext();
        }

        public DbSet<ProductModels> Products { get; set; }
        public DbSet<CategoryModels> Categories { get; set; }
        public DbSet<OrderModels> Orders { get; set; }
        public DbSet<OrderPositionModels> OrderProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<ProjectDbContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}