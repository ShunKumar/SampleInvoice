namespace ApplicationModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSubtotalAndTaxvalueColumnIntoInvoiceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GrubbrrInvoices", "SubTotal", c => c.Double(nullable: false));
            AddColumn("dbo.GrubbrrInvoices", "TotalTax", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GrubbrrInvoices", "TotalTax");
            DropColumn("dbo.GrubbrrInvoices", "SubTotal");
        }
    }
}
