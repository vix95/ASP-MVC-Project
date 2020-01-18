namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrderModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderModels", "Order_phone", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.OrderModels", "Order_address_number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderModels", "Order_address_number", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.OrderModels", "Order_phone", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
