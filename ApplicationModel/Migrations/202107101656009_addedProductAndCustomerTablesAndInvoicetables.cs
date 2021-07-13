namespace ApplicationModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedProductAndCustomerTablesAndInvoicetables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 20),
                        LastName = c.String(maxLength: 20),
                        PhoneNumber = c.String(),
                        Email = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GrubbrrInvoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InVoiceNumber = c.String(),
                        CustomerId = c.Int(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        InvoiceStatus = c.Int(nullable: false),
                        InvoiceNote = c.String(maxLength: 1000),
                        InVoiceDocument = c.String(),
                        GrandTotal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.ProductGrubbrrInvoiceMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductDescription = c.String(maxLength: 500),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Tax = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GrubbrrInvoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(maxLength: 100),
                        Price = c.Double(nullable: false),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductGrubbrrInvoiceMappings", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductGrubbrrInvoiceMappings", "InvoiceId", "dbo.GrubbrrInvoices");
            DropForeignKey("dbo.GrubbrrInvoices", "CustomerId", "dbo.Customers");
            DropIndex("dbo.ProductGrubbrrInvoiceMappings", new[] { "ProductId" });
            DropIndex("dbo.ProductGrubbrrInvoiceMappings", new[] { "InvoiceId" });
            DropIndex("dbo.GrubbrrInvoices", new[] { "CustomerId" });
            DropTable("dbo.Products");
            DropTable("dbo.ProductGrubbrrInvoiceMappings");
            DropTable("dbo.GrubbrrInvoices");
            DropTable("dbo.Customers");
        }
    }
}
