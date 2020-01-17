namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedorderstatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderModels", "Order_email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderModels", "Order_email", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
