namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        basketId = c.String(maxLength: 128),
                        productId = c.String(),
                        quantity = c.Int(nullable: false),
                        createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.basketId)
                .Index(t => t.basketId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        userId = c.String(),
                        firstName = c.String(),
                        lastName = c.String(),
                        email = c.String(),
                        street = c.String(),
                        city = c.String(),
                        state = c.String(),
                        zipCode = c.String(),
                        createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        orderId = c.String(maxLength: 128),
                        productId = c.String(),
                        productName = c.String(),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        image = c.String(),
                        quantity = c.Int(nullable: false),
                        createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.orderId)
                .Index(t => t.orderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        firstName = c.String(),
                        surName = c.String(),
                        email = c.String(),
                        street = c.String(),
                        city = c.String(),
                        state = c.String(),
                        zipCode = c.String(),
                        orderStatus = c.String(),
                        createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 20),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(),
                        Image = c.String(),
                        createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Category = c.String(),
                        createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "orderId", "dbo.Orders");
            DropForeignKey("dbo.BasketItems", "basketId", "dbo.Baskets");
            DropIndex("dbo.OrderItems", new[] { "orderId" });
            DropIndex("dbo.BasketItems", new[] { "basketId" });
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Customers");
            DropTable("dbo.BasketItems");
            DropTable("dbo.Baskets");
        }
    }
}
