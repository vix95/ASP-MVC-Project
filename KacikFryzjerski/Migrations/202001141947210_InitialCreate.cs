namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category_name = c.String(nullable: false, maxLength: 100),
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
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemModels", t => t.OrderItem_itemModels_Id)
                .ForeignKey("dbo.OrderModels", t => t.OrderItem_orderModels_Id)
                .Index(t => t.OrderItem_itemModels_Id)
                .Index(t => t.OrderItem_orderModels_Id);
            
            CreateTable(
                "dbo.OrderModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order_name = c.String(nullable: false, maxLength: 100),
                        Order_surname = c.String(nullable: false, maxLength: 100),
                        Order_city = c.String(nullable: false, maxLength: 100),
                        Order_postcode = c.String(nullable: false, maxLength: 7),
                        Order_address = c.String(nullable: false, maxLength: 100),
                        Order_address_number = c.String(nullable: false, maxLength: 100),
                        Order_phone = c.String(nullable: false, maxLength: 100),
                        Order_email = c.String(nullable: false, maxLength: 100),
                        Order_ordered_at = c.DateTime(nullable: false),
                        Order_order_status = c.Int(nullable: false),
                        Order_total_order_price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItemModels", "OrderItem_orderModels_Id", "dbo.OrderModels");
            DropForeignKey("dbo.OrderItemModels", "OrderItem_itemModels_Id", "dbo.ItemModels");
            DropIndex("dbo.OrderItemModels", new[] { "OrderItem_orderModels_Id" });
            DropIndex("dbo.OrderItemModels", new[] { "OrderItem_itemModels_Id" });
            DropTable("dbo.OrderModels");
            DropTable("dbo.OrderItemModels");
            DropTable("dbo.ItemModels");
            DropTable("dbo.CategoryModels");
        }
    }
}
