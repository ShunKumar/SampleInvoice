using ApplicationModel.Models;
using Repository;
using Repository.Interfaces;
using Services;
using Services.Interfaces;
using System.Data.Entity;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace InvoiceDemo
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<AppDbContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork>(); 
            container.RegisterType<IDemoInvoiceServices, DemoInvoice>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}