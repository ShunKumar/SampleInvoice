using ApplicationModel.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductGrubbrrInvoiceMappingRepository : GenericRepository<ProductGrubbrrInvoiceMapping>, IProductGrubbrrInvoiceMappingRepository
    {
        public ProductGrubbrrInvoiceMappingRepository(AppDbContext _dbContext):base(_dbContext)
        {
            
        }
    }
}
