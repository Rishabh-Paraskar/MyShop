namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedOrders : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "orderId", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "orderId" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
        }
    }
}
