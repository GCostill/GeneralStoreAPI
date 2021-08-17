namespace GeneralStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustedNumberInInventoryAndIsInStockPropertiesInProductClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Product_SKU", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "Product_SKU");
            AddForeignKey("dbo.Products", "Product_SKU", "dbo.Products", "SKU");
            DropColumn("dbo.Products", "NumberInInventory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "NumberInInventory", c => c.Int(nullable: false));
            DropForeignKey("dbo.Products", "Product_SKU", "dbo.Products");
            DropIndex("dbo.Products", new[] { "Product_SKU" });
            DropColumn("dbo.Products", "Product_SKU");
        }
    }
}
