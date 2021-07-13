using ApplicationModel.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CustomerRepository : GenericRepository<Customer> , ICustomerRepository
    {
        public CustomerRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
