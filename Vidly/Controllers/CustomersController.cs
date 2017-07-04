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

        public ActionResult New()
        {
            var membershipTypes = this.context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
            {
                this.context.Customers.Add(customer);
            }
            else
            {
                var customerFromDb = this.context.Customers.Single(c => c.Id == customer.Id);
                customerFromDb.Name = customer.Name;
                customerFromDb.BirthDate = customer.BirthDate;
                customerFromDb.MembershipType = customer.MembershipType;
                customerFromDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            
            this.context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Index()
        {
            var customers = this.context.Customers.Include("MembershipType").ToList();
            return View(customers);
        }

        public ActionResult Edit(int id)
        {
            var customer = this.context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = this.context.MembershipTypes.ToList(),
            };
            return View("CustomerForm", viewModel);
        }
    }
}