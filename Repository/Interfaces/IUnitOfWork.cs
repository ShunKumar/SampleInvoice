using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }

        IProductRepository ProductRepository { get; }

        IGrubbrrInvoiceRepository GrubbrrInvoiceRepository { get; }

        IProductGrubbrrInvoiceMappingRepository ProductGrubbrrInvoiceMappingRepository { get; }

        void Save();
    }
}
