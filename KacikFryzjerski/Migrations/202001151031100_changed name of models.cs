namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changednameofmodels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemModels", "Category_Id", "dbo.CategoryModels");
            DropForeignKey("dbo.OrderItemModels", "OrderItem_itemModels_Id", "dbo.ItemModels");
            DropForeignKey("dbo.OrderItemModels", "OrderItem_orderModels_Id", "dbo.OrderModels");
            DropIndex("dbo.ItemModels", new[] { "Category_Id" });
            DropIndex("dbo.OrderItemModels", new[] { "OrderItem_itemModels_Id" });
            DropIndex("dbo.OrderItemModels", new[] { "OrderItem_orderModels_Id" });
            CreateTable(
                "dbo.ProductModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Product_category_id = c.Int(nullable: false),
                        Product_name = c.String(nullable: false, maxLength: 100),
                        Product_description = c.String(nullable: false, maxLength: 250),
                        Product_price = c.Double(nullable: false),
                        Product_image_path = c.String(maxLength: 200),
                        Product_created_at = c.DateTime(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryModels", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderModels", t => t.OrderProduct_orderModels_Id)
                .ForeignKey("dbo.ProductModels", t => t.OrderProduct_productsModels_Id)
                .Index(t => t.OrderProduct_orderModels_Id)
                .Index(t => t.OrderProduct_productsModels_Id);
            
            DropTable("dbo.ItemModels");
            DropTable("dbo.OrderItemModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderItemModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderItem_order_id = c.Int(nullable: false),
                        OrderItem_item_id = c.Int(nullable: false),
                        OrderItem_quantity = c.Int(nullable: false),
                        Orderitem_price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderItem_itemModels_Id = c.Int(),
                        OrderItem_orderModels_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Item_category_id = c.Int(nullable: false),
                        Item_name = c.String(nullable: false, maxLength: 100),
                        Item_description = c.String(nullable: false, maxLength: 250),
                        Item_price = c.Double(nullable: false),
                        Item_image_path = c.String(maxLength: 200),
                        Item_created_at = c.DateTime(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.OrderProductModels", "OrderProduct_productsModels_Id", "dbo.ProductModels");
            DropForeignKey("dbo.OrderProductModels", "OrderProduct_orderModels_Id", "dbo.OrderModels");
            DropForeignKey("dbo.ProductModels", "Category_Id", "dbo.CategoryModels");
            DropIndex("dbo.OrderProductModels", new[] { "OrderProduct_productsModels_Id" });
            DropIndex("dbo.OrderProductModels", new[] { "OrderProduct_orderModels_Id" });
            DropIndex("dbo.ProductModels", new[] { "Category_Id" });
            DropTable("dbo.OrderProductModels");
            DropTable("dbo.ProductModels");
            CreateIndex("dbo.OrderItemModels", "OrderItem_orderModels_Id");
            CreateIndex("dbo.OrderItemModels", "OrderItem_itemModels_Id");
            CreateIndex("dbo.ItemModels", "Category_Id");
            AddForeignKey("dbo.OrderItemModels", "OrderItem_orderModels_Id", "dbo.OrderModels", "Id");
            AddForeignKey("dbo.OrderItemModels", "OrderItem_itemModels_Id", "dbo.ItemModels", "Id");
            AddForeignKey("dbo.ItemModels", "Category_Id", "dbo.CategoryModels", "Id");
        }
    }
}
