namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBasket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Basket_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.Basket_Id)
                .Index(t => t.Basket_Id);
            
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        basketId = c.String(),
                        productId = c.String(),
                        quantity = c.String(),
                        createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Baskets", "Basket_Id", "dbo.Baskets");
            DropIndex("dbo.Baskets", new[] { "Basket_Id" });
            DropTable("dbo.BasketItems");
            DropTable("dbo.Baskets");
        }
    }
}
