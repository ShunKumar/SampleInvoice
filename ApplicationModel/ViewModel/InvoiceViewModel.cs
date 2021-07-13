using ApplicationModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel.ViewModel
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel()
        {
            ProductList = new List<ProductList>();
        }
        public int id { get; set; }
        public int customerId { get; set; }
        public string invoiceNumber { get; set; }
        public string invoiceDate { get; set; }
        public string invoiceDueDate { get; set; }
        public InvoiceStatus status { get; set; }
        public string invoiceNote { get; set; }
        public string subTotal { get; set; }
        public string taxAmount { get; set; }
        public string grandTotal { get; set; }
        public string invoiceAttachment { get; set; }
        public List<ProductList> ProductList { get; set; }
    }

    public class ProductList
    {
        public int id { get; set; }
        public int productId { get; set; }
        public string description { get; set; }
        public string productPrice { get; set; }
        public int quantity { get; set; }
        public string tax { get; set; }
        public string totalAmount { get; set; }
    }
    

}
