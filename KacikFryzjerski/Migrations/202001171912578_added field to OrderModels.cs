namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfieldtoOrderModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderModels", "UserId", c => c.String());
            AlterColumn("dbo.OrderModels", "Order_phone", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderModels", "Order_phone", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.OrderModels", "UserId");
        }
    }
}
