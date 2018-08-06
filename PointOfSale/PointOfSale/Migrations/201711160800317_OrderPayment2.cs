namespace PointOfSale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderPayment2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropColumn("dbo.Orders", "UserId");
            RenameColumn(table: "dbo.Orders", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Orders", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Orders", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "UserId" });
            AlterColumn("dbo.Orders", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Orders", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "User_Id");
            AddForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
