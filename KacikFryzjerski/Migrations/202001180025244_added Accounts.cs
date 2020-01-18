namespace KacikFryzjerski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAccounts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account_email = c.String(),
                        AccountData_Name = c.String(),
                        AccountData_Surname = c.String(),
                        AccountData_Address = c.String(),
                        AccountData_City = c.String(),
                        AccountData_Postcode = c.String(),
                        AccountData_Phone = c.String(),
                        AccountData_Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.OrderModels", "Order_User_id", c => c.String());
            AddColumn("dbo.OrderModels", "AccountModels_Id", c => c.Int());
            CreateIndex("dbo.OrderModels", "AccountModels_Id");
            AddForeignKey("dbo.OrderModels", "AccountModels_Id", "dbo.AccountModels", "Id");
            DropColumn("dbo.OrderModels", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderModels", "UserId", c => c.String());
            DropForeignKey("dbo.OrderModels", "AccountModels_Id", "dbo.AccountModels");
            DropIndex("dbo.OrderModels", new[] { "AccountModels_Id" });
            DropColumn("dbo.OrderModels", "AccountModels_Id");
            DropColumn("dbo.OrderModels", "Order_User_id");
            DropTable("dbo.AccountModels");
        }
    }
}
