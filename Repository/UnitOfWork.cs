using ApplicationModel.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Property and Constructor region

        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Repository instance creation 

        public ICustomerRepository CustomerRepository
        {
            get
            {
                return new CustomerRepository(_dbContext);
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return new ProductRepository(_dbContext);
            }
        }

        public IGrubbrrInvoiceRepository GrubbrrInvoiceRepository
        {
            get
            {
                return new GrubbrrInvoiceRepository(_dbContext);
            }
        }

        public IProductGrubbrrInvoiceMappingRepository ProductGrubbrrInvoiceMappingRepository
        {
            get
            {
                return new ProductGrubbrrInvoiceMappingRepository(_dbContext);
            }
        }

        #endregion
        
        /// <summary>
        /// Common method to save all table data
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
