namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedmodels : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ItemModels", newName: "ProductModels");
            DropForeignKey("dbo.OrderItemModels", "OrderItem_itemModels_Id", "dbo.ItemModels");
            DropForeignKey("dbo.OrderItemModels", "OrderItem_orderModels_Id", "dbo.OrderModels");
            DropIndex("dbo.OrderItemModels", new[] { "OrderItem_itemModels_Id" });
            DropIndex("dbo.OrderItemModels", new[] { "OrderItem_orderModels_Id" });
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
            
            AddColumn("dbo.ProductModels", "Product_category_id", c => c.Int(nullable: false));
            AddColumn("dbo.ProductModels", "Product_name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.ProductModels", "Product_description", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.ProductModels", "Product_price", c => c.Double(nullable: false));
            AddColumn("dbo.ProductModels", "Product_image_path", c => c.String(maxLength: 200));
            AddColumn("dbo.ProductModels", "Product_created_at", c => c.DateTime(nullable: false));
            DropColumn("dbo.ProductModels", "Item_category_id");
            DropColumn("dbo.ProductModels", "Item_name");
            DropColumn("dbo.ProductModels", "Item_description");
            DropColumn("dbo.ProductModels", "Item_price");
            DropColumn("dbo.ProductModels", "Item_image_path");
            DropColumn("dbo.ProductModels", "Item_created_at");
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
            
            AddColumn("dbo.ProductModels", "Item_created_at", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductModels", "Item_image_path", c => c.String(maxLength: 200));
            AddColumn("dbo.ProductModels", "Item_price", c => c.Double(nullable: false));
            AddColumn("dbo.ProductModels", "Item_description", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.ProductModels", "Item_name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.ProductModels", "Item_category_id", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderProductModels", "OrderProduct_productsModels_Id", "dbo.ProductModels");
            DropForeignKey("dbo.OrderProductModels", "OrderProduct_orderModels_Id", "dbo.OrderModels");
            DropIndex("dbo.OrderProductModels", new[] { "OrderProduct_productsModels_Id" });
            DropIndex("dbo.OrderProductModels", new[] { "OrderProduct_orderModels_Id" });
            DropColumn("dbo.ProductModels", "Product_created_at");
            DropColumn("dbo.ProductModels", "Product_image_path");
            DropColumn("dbo.ProductModels", "Product_price");
            DropColumn("dbo.ProductModels", "Product_description");
            DropColumn("dbo.ProductModels", "Product_name");
            DropColumn("dbo.ProductModels", "Product_category_id");
            DropTable("dbo.OrderProductModels");
            CreateIndex("dbo.OrderItemModels", "OrderItem_orderModels_Id");
            CreateIndex("dbo.OrderItemModels", "OrderItem_itemModels_Id");
            AddForeignKey("dbo.OrderItemModels", "OrderItem_orderModels_Id", "dbo.OrderModels", "Id");
            AddForeignKey("dbo.OrderItemModels", "OrderItem_itemModels_Id", "dbo.ItemModels", "Id");
            RenameTable(name: "dbo.ProductModels", newName: "ItemModels");
        }
    }
}
