namespace PointOfSale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PaymentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "PaymentId");
            AddForeignKey("dbo.Orders", "PaymentId", "dbo.Payments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "PaymentId", "dbo.Payments");
            DropIndex("dbo.Orders", new[] { "PaymentId" });
            DropColumn("dbo.Orders", "PaymentId");
        }
    }
}
