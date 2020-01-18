namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedProductmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductModels", "Product_is_bestseller", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductModels", "Product_is_bestseller");
        }
    }
}
