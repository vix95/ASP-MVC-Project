namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtypeofidinAccountModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountModels", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccountModels", "UserId");
        }
    }
}
