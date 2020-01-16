namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcartmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderProductModels", "OrderProduct_orderModels_Id", "dbo.OrderModels");
            DropForeignKey("dbo.OrderProductModels", "OrderProduct_productsModels_Id", "dbo.ProductModels");
            DropIndex("dbo.OrderProductModels", new[] { "OrderProduct_orderModels_Id" });
            DropIndex("dbo.OrderProductModels", new[] { "OrderProduct_productsModels_Id" });
            CreateTable(
                "dbo.OrderPositionModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderPosition_order_id = c.Int(nullable: false),
                        OrderPosition_product_id = c.Int(nullable: false),
                        OrderPosition_quantity = c.Int(nullable: false),
                        OrderPosition_price = c.Double(nullable: false),
                        Order_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderModels", t => t.Order_Id)
                .ForeignKey("dbo.ProductModels", t => t.Product_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.Product_Id);
            
            AlterColumn("dbo.OrderModels", "Order_total_order_price", c => c.Double(nullable: false));
            DropTable("dbo.OrderProductModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderProductModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderProduct_order_id = c.Int(nullable: false),
                        OrderProduct_item_id = c.Int(nullable: false),
                        OrderProduct_quantity = c.Int(nullable: false),
                        OrderProduct_price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderProduct_orderModels_Id = c.Int(),
                        OrderProduct_productsModels_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.OrderPositionModels", "Product_Id", "dbo.ProductModels");
            DropForeignKey("dbo.OrderPositionModels", "Order_Id", "dbo.OrderModels");
            DropIndex("dbo.OrderPositionModels", new[] { "Product_Id" });
            DropIndex("dbo.OrderPositionModels", new[] { "Order_Id" });
            AlterColumn("dbo.OrderModels", "Order_total_order_price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropTable("dbo.OrderPositionModels");
            CreateIndex("dbo.OrderProductModels", "OrderProduct_productsModels_Id");
            CreateIndex("dbo.OrderProductModels", "OrderProduct_orderModels_Id");
            AddForeignKey("dbo.OrderProductModels", "OrderProduct_productsModels_Id", "dbo.ProductModels", "Id");
            AddForeignKey("dbo.OrderProductModels", "OrderProduct_orderModels_Id", "dbo.OrderModels", "Id");
        }
    }
}
