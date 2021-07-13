using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel.Models
{
    public class GrubbrrInvoice
    {
        public int Id { get; set; }
        public string InVoiceNumber { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        [MaxLength(1000)]
        public string InvoiceNote { get; set; }
        public string InVoiceDocument { get; set; }
        public double SubTotal { get; set; }
        public double TotalTax { get; set; }
        public double GrandTotal { get; set; }
    }

    public enum InvoiceStatus
    {
        Draft,
        Sent,
        Paid
    }
}
