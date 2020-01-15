namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfieldforItemModels : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ItemModels", name: "CategoryModels_Id", newName: "Category_Id");
            RenameIndex(table: "dbo.ItemModels", name: "IX_CategoryModels_Id", newName: "IX_Category_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ItemModels", name: "IX_Category_Id", newName: "IX_CategoryModels_Id");
            RenameColumn(table: "dbo.ItemModels", name: "Category_Id", newName: "CategoryModels_Id");
        }
    }
}
