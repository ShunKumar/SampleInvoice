using ApplicationModel.Models;
using ApplicationModel.ViewModel;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;

namespace InvoiceDemo.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IDemoInvoiceServices demoInvoiceServices;
        public static string attachmentPath = "~/Attachments/";
        public static string attachmentDbPath = "/Attachments/";
        public InvoiceController(IDemoInvoiceServices _demoInvoiceServices)
        {
            demoInvoiceServices = _demoInvoiceServices;
        }
        // GET: InvoiceD:\SampleDemo\InvoiceDemo\InvoiceDemo\Attachments\
        public ActionResult Index()
        {
            ViewBag.CurrentDate = DateTime.Now;
            return View();
        }
       
        public JsonResult GetGrubbrrInvoiceList(JqueryDatatableParam param)
        {
            int index = 1;
            List<GrubbrrInvoiceViewModel> grubbrrInvoiceViewModels = demoInvoiceServices.GetGrubbrrInvoices(param);
            var displayResult = (grubbrrInvoiceViewModels != null && grubbrrInvoiceViewModels.Count > 0) ? grubbrrInvoiceViewModels.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList() : grubbrrInvoiceViewModels=new List<GrubbrrInvoiceViewModel>(); 
            var totalRecords = grubbrrInvoiceViewModels != null?grubbrrInvoiceViewModels.Count():0;
            
            foreach (var item in displayResult)
            {
                item.Index = index;
                index += 1;
            }
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult GetCutomers()
        {
            List<Customer> custopmers = demoInvoiceServices.GetCutomers();
            return Json(custopmers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProducts()
        {
            List<Product> products = demoInvoiceServices.GetProducts();
            return Json(products, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetProductById(int id)
        {
            Product product = demoInvoiceServices.GetProductById(id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateOrUpdateInvoiceViewModel()
        {
            string path = null;
            string invoiceData = Request["invoiceModel"];
            HttpPostedFileBase httpPostedFile = Request.Files["invoiceAttachment"];
            //Extract Image File Name.
            if (httpPostedFile!=null)
            {
                string spacedRemovedFileName = httpPostedFile.FileName;
                spacedRemovedFileName = System.Text.RegularExpressions.Regex.Replace(spacedRemovedFileName, @"\s", "");
                string fileName = Guid.NewGuid() + spacedRemovedFileName;
                path = attachmentPath + fileName;
                //method to check folder exist or not -- if not exist create
                DirectoryPathCheckAndCreate(attachmentPath);
                httpPostedFile.SaveAs(Server.MapPath(attachmentPath + fileName));
                path = attachmentDbPath + fileName;
            }
           
            InvoiceViewModel invoiceViewModel = JsonConvert.DeserializeObject<InvoiceViewModel>(invoiceData);
            demoInvoiceServices.CreateInvoiceData(invoiceViewModel, path);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        public void DirectoryPathCheckAndCreate(string directoryPath)
        {
            if (!Directory.Exists(Server.MapPath(attachmentPath)))
            {
                Directory.CreateDirectory(Server.MapPath(attachmentPath));
            }
        }


        [HttpGet]
        public JsonResult DeleteInvoice(int id)
        {
            demoInvoiceServices.DeleteInvoice(id);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetInvoiceDataByIdToEdit(int id)
        {
            InvoiceViewModel invoiceViewModel = demoInvoiceServices.GetInvoiceDataById(id);
            return Json(invoiceViewModel, JsonRequestBehavior.AllowGet);
        }

    }
}