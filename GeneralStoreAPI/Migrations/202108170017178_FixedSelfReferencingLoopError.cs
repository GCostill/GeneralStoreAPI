namespace GeneralStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedSelfReferencingLoopError : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Product_SKU", "dbo.Products");
            DropForeignKey("dbo.Products", "Transaction_ID", "dbo.Transactions");
            DropIndex("dbo.Products", new[] { "Product_SKU" });
            DropIndex("dbo.Products", new[] { "Transaction_ID" });
            AddColumn("dbo.Products", "NumberInInventory", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "ItemCount", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Product_SKU");
            DropColumn("dbo.Products", "Transaction_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Transaction_ID", c => c.Int());
            AddColumn("dbo.Products", "Product_SKU", c => c.String(maxLength: 128));
            DropColumn("dbo.Transactions", "ItemCount");
            DropColumn("dbo.Products", "NumberInInventory");
            CreateIndex("dbo.Products", "Transaction_ID");
            CreateIndex("dbo.Products", "Product_SKU");
            AddForeignKey("dbo.Products", "Transaction_ID", "dbo.Transactions", "ID");
            AddForeignKey("dbo.Products", "Product_SKU", "dbo.Products", "SKU");
        }
    }
}
