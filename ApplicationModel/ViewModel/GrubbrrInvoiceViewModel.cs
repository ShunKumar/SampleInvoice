using ApplicationModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel.ViewModel
{
    public class GrubbrrInvoiceViewModel
    {
        public int Id { get; set; }
        public int Index { get; set; } 
        public string InVoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string InvoiceDate { get; set; }
        public string DueDate { get; set; }
        public string InvoiceStatus { get; set; }
        public double GrandTotal { get; set; }
    }
}
