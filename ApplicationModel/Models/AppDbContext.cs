using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext():base("InvoiceDemo")
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<GrubbrrInvoice> GrubbrrInvoices { get; set; }
        public DbSet<ProductGrubbrrInvoiceMapping> ProductGrubbrrInvoiceMappings { get; set; }
    }

    
}
