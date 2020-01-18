namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedOrderPositionModelsaslist : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderPositionModels", "Product_Id", "dbo.ProductModels");
            DropIndex("dbo.OrderPositionModels", new[] { "Product_Id" });
            AddColumn("dbo.ProductModels", "OrderPositionModels_Id", c => c.Int());
            CreateIndex("dbo.ProductModels", "OrderPositionModels_Id");
            AddForeignKey("dbo.ProductModels", "OrderPositionModels_Id", "dbo.OrderPositionModels", "Id");
            DropColumn("dbo.OrderPositionModels", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderPositionModels", "Product_Id", c => c.Int());
            DropForeignKey("dbo.ProductModels", "OrderPositionModels_Id", "dbo.OrderPositionModels");
            DropIndex("dbo.ProductModels", new[] { "OrderPositionModels_Id" });
            DropColumn("dbo.ProductModels", "OrderPositionModels_Id");
            CreateIndex("dbo.OrderPositionModels", "Product_Id");
            AddForeignKey("dbo.OrderPositionModels", "Product_Id", "dbo.ProductModels", "Id");
        }
    }
}
