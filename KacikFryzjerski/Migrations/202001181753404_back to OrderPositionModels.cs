namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class backtoOrderPositionModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductModels", "OrderPositionModels_Id", "dbo.OrderPositionModels");
            DropIndex("dbo.ProductModels", new[] { "OrderPositionModels_Id" });
            AddColumn("dbo.OrderPositionModels", "Product_Id", c => c.Int());
            CreateIndex("dbo.OrderPositionModels", "Product_Id");
            AddForeignKey("dbo.OrderPositionModels", "Product_Id", "dbo.ProductModels", "Id");
            DropColumn("dbo.ProductModels", "OrderPositionModels_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductModels", "OrderPositionModels_Id", c => c.Int());
            DropForeignKey("dbo.OrderPositionModels", "Product_Id", "dbo.ProductModels");
            DropIndex("dbo.OrderPositionModels", new[] { "Product_Id" });
            DropColumn("dbo.OrderPositionModels", "Product_Id");
            CreateIndex("dbo.ProductModels", "OrderPositionModels_Id");
            AddForeignKey("dbo.ProductModels", "OrderPositionModels_Id", "dbo.OrderPositionModels", "Id");
        }
    }
}
