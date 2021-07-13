using ApplicationModel.Models;
using ApplicationModel.ViewModel;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DemoInvoice : IDemoInvoiceServices
    {
        #region Property and Constructor region

        private readonly AppDbContext _dbContext;
        private readonly IUnitOfWork db;

        public DemoInvoice(AppDbContext dbContext,IUnitOfWork unitOfWork )
        {
            _dbContext = dbContext;
            db = unitOfWork;
        }

        #endregion

        #region Public helper method regions to generate invoice

        /// <summary>
        /// helper method to ge the Customer list -  to show while generate invoice
        /// </summary>
        /// <returns></returns>
        public Product GetProductById(int id)
        {
            Product product = db.ProductRepository.GetByID(id);
            return product;
        }

        /// <summary>
        /// helper method to ge the Customer list -  to show while generate invoice
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCutomers()
        {
            List<Customer> customerList = db.CustomerRepository.GetAll().ToList();
            return customerList;
        }

        /// <summary>
        /// helper method to ge the Product list -  to show while generate invoice
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProducts()
        {
            List<Product> productList = db.ProductRepository.GetAll().ToList();
            return productList;
        }

        /// <summary>
        /// helper method to ge the Product list -  to show invoice Data table 
        /// </summary>
        /// <returns></returns>
        public List<GrubbrrInvoiceViewModel> GetGrubbrrInvoices(JqueryDatatableParam param)
        {
            List<GrubbrrInvoiceViewModel> grubbrrInvoiceViewModels = null;
            List<GrubbrrInvoice> grubbrrInvoices = db.GrubbrrInvoiceRepository.Get(includeproperties: "Customer").ToList();
            if (grubbrrInvoices.Count>0)
            {
                GrubbrrInvoiceViewModel grubbrrInvoiceViewModel = null;
                grubbrrInvoiceViewModels = new List<GrubbrrInvoiceViewModel>();
                foreach (GrubbrrInvoice grubbrr in grubbrrInvoices)
                {
                    grubbrrInvoiceViewModel = new GrubbrrInvoiceViewModel();
                    grubbrrInvoiceViewModel.Id = grubbrr.Id;
                    grubbrrInvoiceViewModel.InVoiceNumber = grubbrr.InVoiceNumber;
                    grubbrrInvoiceViewModel.CustomerName = grubbrr.Customer.FirstName + grubbrr.Customer.LastName;
                    grubbrrInvoiceViewModel.InvoiceDate = grubbrr.InvoiceDate.ToString("dd'/'MM'/'yyyy");
                    grubbrrInvoiceViewModel.DueDate = grubbrr.DueDate.ToString("dd'/'MM'/'yyyy");
                    grubbrrInvoiceViewModel.InvoiceStatus = grubbrr.InvoiceStatus.ToString();
                    grubbrrInvoiceViewModel.GrandTotal = grubbrr.GrandTotal;
                    grubbrrInvoiceViewModels.Add(grubbrrInvoiceViewModel);
                }
                //search filter
                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    grubbrrInvoiceViewModels = grubbrrInvoiceViewModels.Where(x => x.CustomerName.ToLower().Contains(param.sSearch.ToLower()) || x.InVoiceNumber.ToLower().Contains(param.sSearch.ToLower()) || x.InvoiceDate.ToLower().Contains(param.sSearch.ToLower()) || x.DueDate.ToLower().Contains(param.sSearch.ToLower())).ToList();
                }
                //sort filter
                var sortColumnIndex = param.iSortCol_0;
                var sortDirection = param.sSortDir_0;
                if (sortColumnIndex == 2)
                {
                    grubbrrInvoiceViewModels = sortDirection == "asc" ? grubbrrInvoiceViewModels.OrderBy(c => c.InVoiceNumber).ToList
                        (): grubbrrInvoiceViewModels.OrderByDescending(c => c.InVoiceNumber).ToList();
                }
                else if (sortColumnIndex == 3)
                {
                    grubbrrInvoiceViewModels = sortDirection == "asc" ? grubbrrInvoiceViewModels.OrderBy(c => c.CustomerName).ToList
                        () : grubbrrInvoiceViewModels.OrderByDescending(c => c.CustomerName).ToList();
                }
                else if (sortColumnIndex == 4)
                {
                    grubbrrInvoiceViewModels = sortDirection == "asc" ? grubbrrInvoiceViewModels.OrderBy(c => c.InvoiceDate).ToList() : grubbrrInvoiceViewModels.OrderByDescending(c => c.InvoiceDate).ToList();
                }
                else if (sortColumnIndex == 5)
                {
                    grubbrrInvoiceViewModels = sortDirection == "asc" ? grubbrrInvoiceViewModels.OrderBy(c => c.DueDate).ToList() : grubbrrInvoiceViewModels.OrderByDescending(c => c.DueDate).ToList();
                }
                else if (sortColumnIndex == 6)
                {
                    grubbrrInvoiceViewModels = sortDirection == "asc" ? grubbrrInvoiceViewModels.OrderBy(c => c.InvoiceStatus).ToList() : grubbrrInvoiceViewModels.OrderByDescending(c => c.InvoiceStatus).ToList();
                }
                else if (sortColumnIndex == 7)
                {
                    grubbrrInvoiceViewModels = sortDirection == "asc" ? grubbrrInvoiceViewModels.OrderBy(c => c.GrandTotal).ToList() : grubbrrInvoiceViewModels.OrderByDescending(c => c.GrandTotal).ToList();
                }
                else
                {
                    Func<GrubbrrInvoiceViewModel, string> orderingFunction = e => sortColumnIndex == 0 ? e.InVoiceNumber : sortColumnIndex == 1 ? e.InVoiceNumber : e.CustomerName;
                    grubbrrInvoiceViewModels = sortDirection == "asc" ? grubbrrInvoiceViewModels.OrderBy(orderingFunction).ToList() : grubbrrInvoiceViewModels.OrderByDescending(orderingFunction).ToList();
                }
            }
            return grubbrrInvoiceViewModels;
        }

        /// <summary>
        /// helper method to create invoice data
        /// </summary>
        /// <param name="invoiceViewModel"></param>
        public void CreateInvoiceData(InvoiceViewModel invoiceViewModel,string attachedFile=null)
        {
            bool isGrubbrrInsert = false;
            bool isProductInsert = false;
            GrubbrrInvoice grubbrrInvoice = null;
            if (invoiceViewModel.id==0)
            {
                isGrubbrrInsert = true;
                grubbrrInvoice = new GrubbrrInvoice();
            }
            else
            {
                grubbrrInvoice = db.GrubbrrInvoiceRepository.GetByID(invoiceViewModel.id);
                if (attachedFile != null && (!string.IsNullOrEmpty(grubbrrInvoice.InVoiceDocument)))
                {
                    if (File.Exists(grubbrrInvoice.InVoiceDocument))
                    {
                        File.Delete(grubbrrInvoice.InVoiceDocument);
                    }
                }
            }
            grubbrrInvoice.CustomerId = invoiceViewModel.customerId;
            grubbrrInvoice.InVoiceNumber = invoiceViewModel.invoiceNumber;
            grubbrrInvoice.InvoiceDate = DateTime.Parse(invoiceViewModel.invoiceDate);
            grubbrrInvoice.DueDate = DateTime.Parse(invoiceViewModel.invoiceDueDate);
            grubbrrInvoice.InvoiceStatus = invoiceViewModel.status;
            grubbrrInvoice.InvoiceNote = invoiceViewModel.invoiceNote;
            grubbrrInvoice.SubTotal = Convert.ToDouble(invoiceViewModel.subTotal);
            grubbrrInvoice.TotalTax = Convert.ToDouble(invoiceViewModel.taxAmount);
            grubbrrInvoice.GrandTotal = Convert.ToDouble(invoiceViewModel.grandTotal);
            if (attachedFile != null)
            {
                grubbrrInvoice.InVoiceDocument = attachedFile;
            }
            if (isGrubbrrInsert)
            {
                db.GrubbrrInvoiceRepository.Insert(grubbrrInvoice);
            }
            db.Save();
            ProductGrubbrrInvoiceMapping productGrubbrrInvoiceMapping = null;
            int currentProductCount = invoiceViewModel.ProductList.Count();
            List<int> existingProducts = db.ProductGrubbrrInvoiceMappingRepository.Get().Where(x => x.InvoiceId == grubbrrInvoice.Id).Select(x=>x.Id).ToList();
            List<int> totalAddedProductIds = new List<int>();
            foreach (ProductList productList in invoiceViewModel.ProductList)
            {
                isProductInsert = false;
                if (productList.id == 0)
                {
                    isProductInsert = true;
                    productGrubbrrInvoiceMapping = new ProductGrubbrrInvoiceMapping();
                }
                else
                {
                    totalAddedProductIds.Add(productList.id);
                    productGrubbrrInvoiceMapping = db.ProductGrubbrrInvoiceMappingRepository.GetByID(productList.id);
                }
                productGrubbrrInvoiceMapping.InvoiceId = grubbrrInvoice.Id;
                productGrubbrrInvoiceMapping.ProductId = productList.productId;
                productGrubbrrInvoiceMapping.Price = Convert.ToDouble(productList.productPrice);
                productGrubbrrInvoiceMapping.ProductDescription = productList.description;
                productGrubbrrInvoiceMapping.Quantity = productList.quantity;
                productGrubbrrInvoiceMapping.Tax = Convert.ToDouble(productList.tax);
                productGrubbrrInvoiceMapping.Total = Convert.ToDouble(productList.totalAmount);
                if (isProductInsert)
                {
                    db.ProductGrubbrrInvoiceMappingRepository.Insert(productGrubbrrInvoiceMapping);
                }
                db.Save();
            }
            //already added product while edit invoice might be removed - to delete that remopved project below code added
            if (existingProducts.Count != totalAddedProductIds.Count())
            {
                var exceptResult = existingProducts.Except(totalAddedProductIds);
                foreach(int id in exceptResult)
                {
                    ProductGrubbrrInvoiceMapping removeProductGrubbrrInvoiceMapping = db.ProductGrubbrrInvoiceMappingRepository.GetByID(id);
                    if (removeProductGrubbrrInvoiceMapping!=null)
                    {
                        db.ProductGrubbrrInvoiceMappingRepository.Delete(removeProductGrubbrrInvoiceMapping);
                        db.Save();
                    }
                }
            }
        }

        /// <summary>
        /// helper method to delete invoice
        /// </summary>
        /// <param name="id"></param>
        public void DeleteInvoice(int id)
        {
            List<int> productInvoiceMapIds = db.ProductGrubbrrInvoiceMappingRepository.Get().Where(x => x.InvoiceId == id).Select(x => x.Id).ToList();
            db.ProductGrubbrrInvoiceMappingRepository.DeleteList(productInvoiceMapIds);
            db.Save();
            GrubbrrInvoice grubbrrInvoice = db.GrubbrrInvoiceRepository.GetByID(id);
            db.GrubbrrInvoiceRepository.Delete(grubbrrInvoice);
            db.Save();
        }

        /// <summary>
        /// helper method to get the invoice model for edit screen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InvoiceViewModel GetInvoiceDataById(int id)
        {
            InvoiceViewModel grubbrrInvoiceViewModel = null;
            GrubbrrInvoice grubbrrInvoice = db.GrubbrrInvoiceRepository.GetByID(id);
            if (grubbrrInvoice!=null)
            {
                grubbrrInvoiceViewModel = new InvoiceViewModel();
                ProductList products = null;
                grubbrrInvoiceViewModel.id= grubbrrInvoice.Id;
                grubbrrInvoiceViewModel.customerId = grubbrrInvoice.CustomerId;
                grubbrrInvoiceViewModel.invoiceDate = grubbrrInvoice.InvoiceDate.ToString();
                grubbrrInvoiceViewModel.invoiceDueDate = grubbrrInvoice.DueDate.ToString();
                grubbrrInvoiceViewModel.invoiceNote = grubbrrInvoice.InvoiceNote;
                grubbrrInvoiceViewModel.invoiceNumber = grubbrrInvoice.InVoiceNumber;
                grubbrrInvoiceViewModel.status = grubbrrInvoice.InvoiceStatus;
                grubbrrInvoiceViewModel.subTotal = grubbrrInvoice.SubTotal.ToString();
                grubbrrInvoiceViewModel.taxAmount = grubbrrInvoice.TotalTax.ToString();
                grubbrrInvoiceViewModel.grandTotal = grubbrrInvoice.GrandTotal.ToString();
                grubbrrInvoiceViewModel.invoiceAttachment= grubbrrInvoice.InVoiceDocument;
                List<ProductGrubbrrInvoiceMapping> productList = db.ProductGrubbrrInvoiceMappingRepository.Get().Where(x => x.InvoiceId == id).ToList();
                foreach (ProductGrubbrrInvoiceMapping item in productList)
                {
                    products = new ProductList();
                    products.id = item.Id;
                    products.productId = item.ProductId;
                    products.productPrice = item.Price.ToString();
                    products.description = item.ProductDescription;
                    products.quantity = item.Quantity;
                    products.tax = item.Tax.ToString();
                    products.totalAmount = item.Total.ToString();
                    grubbrrInvoiceViewModel.ProductList.Add(products);
                }
            }
            return grubbrrInvoiceViewModel;
        }
        #endregion
    }
}
