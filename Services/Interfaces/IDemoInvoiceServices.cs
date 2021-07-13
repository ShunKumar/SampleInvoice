using ApplicationModel.Models;
using ApplicationModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDemoInvoiceServices
    {
        Product GetProductById(int id);
        List<Customer> GetCutomers();
        List<Product> GetProducts();
        List<GrubbrrInvoiceViewModel> GetGrubbrrInvoices(JqueryDatatableParam param);
        void CreateInvoiceData(InvoiceViewModel invoiceViewModel,string attachmentFilePath=null);
        void DeleteInvoice(int id);
        InvoiceViewModel GetInvoiceDataById(int id);
        // List<ProductGrubbrrInvoiceMapping> GetProductGrubbrrInvoiceMappings();
    }
}
