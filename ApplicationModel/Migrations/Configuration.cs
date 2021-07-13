namespace ApplicationModel.Migrations
{
    using ApplicationModel.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationModel.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            DateTime currentdate = DateTime.Now;
            //dummy data added for customers table
            IList<Customer> customers = new List<Customer>
            {
                new Customer() { FirstName = "Shun", LastName = "Kumar", PhoneNumber = "+91 7708347266", Email = "techie.shun@gmail.com" },
                new Customer() { FirstName = "Vinoth", LastName = "Kumar", PhoneNumber = "+91 8072893476" },
                new Customer() { FirstName = "Jaya", LastName = "Kumar", PhoneNumber = "+91 7708347265" },
                new Customer() { FirstName = "Prem", LastName = "Kumar", PhoneNumber = "+91 7708347267" }
            };
            context.Customers.AddRange(customers);
            //dummy data added for Product table
            IList<Product> products = new List<Product>
            {
                new Product() { ProductName = "Apple IPhone", Price = 82000.00, Description = "Apple IPhone Description Content" },
                new Product() { ProductName = "Oneplus8 pro", Price = 48000.00, Description = "One plus 8 pro Description Content" },
                new Product() { ProductName = "Samsung S20", Price = 51000.00, Description = "Samsung S20 Description Content" }
            };
            context.Products.AddRange(products);
            //Customer customer_1 = context.Customers.Where(x => x.FirstName == "Shun").Select(x => x).FirstOrDefault();
            //Customer customer_2 = context.Customers.Where(x => x.FirstName == "Vinoth").Select(x => x).FirstOrDefault();
            //Customer customer_3 = context.Customers.Where(x => x.FirstName == "Prem").Select(x => x).FirstOrDefault();
            //Product productId_1 = context.Products.Where(x => x.ProductName == "Apple i Phone").Select(x => x).FirstOrDefault();
            //Product productId_2 = context.Products.Where(x => x.ProductName == "One plus 8 pro").Select(x => x).FirstOrDefault();
            //Product productId_3 = context.Products.Where(x => x.ProductName == "Samsung S20").Select(x => x).FirstOrDefault();

            ////dummy data added for grubbrr invoice table
            //IList<GrubbrrInvoice> grubbrrInvoices = new List<GrubbrrInvoice>
            //{
            //    new GrubbrrInvoice() { InVoiceNumber="Inv-0001", CustomerId=customer_1.Id,InvoiceDate=currentdate ,DueDate= currentdate.AddDays(2),InvoiceStatus = InvoiceStatus.Draft,GrandTotal=82000.00},
            //    new GrubbrrInvoice() { InVoiceNumber="Inv-0002", CustomerId=customer_2.Id,InvoiceDate=currentdate ,DueDate= currentdate.AddDays(3),InvoiceStatus = InvoiceStatus.Sent,GrandTotal=48000.00},
            //    new GrubbrrInvoice() { InVoiceNumber="Inv-0003", CustomerId=customer_3.Id,InvoiceDate=currentdate,DueDate = currentdate.AddDays(3),InvoiceStatus = InvoiceStatus.Sent,GrandTotal=51000.00}

            //};
            //context.GrubbrrInvoices.AddRange(grubbrrInvoices);
            //context.SaveChanges();
            //GrubbrrInvoice grubbrrInvoice_1 = context.GrubbrrInvoices.Where(x => x.InVoiceNumber == "Inv-0001").Select(x => x).FirstOrDefault();
            //GrubbrrInvoice grubbrrInvoice_2 = context.GrubbrrInvoices.Where(x => x.InVoiceNumber == "Inv-0002").Select(x => x).FirstOrDefault();
            //GrubbrrInvoice grubbrrInvoice_3 = context.GrubbrrInvoices.Where(x => x.InVoiceNumber == "Inv-0003").Select(x => x).FirstOrDefault();

            //IList<ProductGrubbrrInvoiceMapping> productGrubbrrInvoiceMappings = new List<ProductGrubbrrInvoiceMapping>
            //{
            //    new ProductGrubbrrInvoiceMapping(){
            //        InvoiceId=grubbrrInvoice_1.Id,ProductId = productId_1.Id,ProductDescription=productId_1.Description,Quantity =1,Price=productId_1.Price,Total =82000.00
            //    },
            //    new ProductGrubbrrInvoiceMapping(){
            //        InvoiceId=grubbrrInvoice_2.Id,ProductId = productId_2.Id,ProductDescription=productId_2.Description,Quantity =1,Price=productId_2.Price,Total =48000.00
            //    },
            //    new ProductGrubbrrInvoiceMapping(){
            //        InvoiceId=grubbrrInvoice_3.Id,ProductId = productId_3.Id,ProductDescription=productId_3.Description,Quantity =1,Price=productId_3.Price,Total =51000.00
            //    },

            //};
            //context.ProductGrubbrrInvoiceMappings.AddRange(productGrubbrrInvoiceMappings);
            //context.SaveChanges();
        }
    }
}
