namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemModels", "CategoryModels_Id", c => c.Int());
            CreateIndex("dbo.ItemModels", "CategoryModels_Id");
            AddForeignKey("dbo.ItemModels", "CategoryModels_Id", "dbo.CategoryModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemModels", "CategoryModels_Id", "dbo.CategoryModels");
            DropIndex("dbo.ItemModels", new[] { "CategoryModels_Id" });
            DropColumn("dbo.ItemModels", "CategoryModels_Id");
        }
    }
}
