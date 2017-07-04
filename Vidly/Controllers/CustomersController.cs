using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext context;

        public CustomersController()
        {
            this.context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            this.context.Dispose();
        }

        // GET: Customer
        public ActionResult Index()
        {
            var customers = this.context.Customers.Include("MembershipType").ToList();
            return View(customers);
        }

        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {
            var customer = this.context.Customers.Include("MembershipType").SingleOrDefault(c => c.Id == id);
            return View(customer);
        }
    }
}