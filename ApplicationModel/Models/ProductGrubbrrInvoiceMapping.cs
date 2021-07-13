using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel.Models
{
    public class ProductGrubbrrInvoiceMapping
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public virtual GrubbrrInvoice GrubbrrInvoice { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [MaxLength(500)]
        public string ProductDescription { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }//this is percentage value
        public double Total { get; set; }
    }
}
