namespace PointOfSale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ItemProducts", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "TotalValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "TotalValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ItemProducts", "Quantity");
            DropColumn("dbo.Orders", "Amount");
        }
    }
}
