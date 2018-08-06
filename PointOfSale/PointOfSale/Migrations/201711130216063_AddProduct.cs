namespace PointOfSale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Payments", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Products", "Price");
        }
    }
}
