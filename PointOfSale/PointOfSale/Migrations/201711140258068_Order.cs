namespace PointOfSale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        DtOrder = c.DateTime(nullable: false),
                        OrderStatus = c.Int(nullable: false),
                        DtPayment = c.DateTime(nullable: false),
                        TotalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ItemProducts",
                c => new
                    {
                        ItemProductId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemProductId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ItemProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ItemProducts", "OrderId", "dbo.Orders");
            DropIndex("dbo.ItemProducts", new[] { "OrderId" });
            DropIndex("dbo.ItemProducts", new[] { "ProductId" });
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropTable("dbo.ItemProducts");
            DropTable("dbo.Orders");
        }
    }
}
