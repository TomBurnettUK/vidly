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
        public List<Customer> Customers { get; set; }

        public CustomersController()
        {
            this.Customers = new List<Customer>
            {
                new Customer() { Id = 1, Name = "Fred" },
                new Customer() { Id = 2, Name = "Mary" },
                new Customer() { Id = 3, Name = "Helen" },
                new Customer() { Id = 4, Name = "Dave" },
                new Customer() { Id = 5, Name = "Steve" },
                new Customer() { Id = 6, Name = "Karen" },
            };
        }

        // GET: Customer
        public ActionResult Index()
        {
            var viewModel = new CustomerListViewModel() { Customers = this.Customers };

            return View(viewModel);
        }

        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {
            var model = this.Customers.Find(customer => customer.Id == id);
            return View(model);
        }
    }
}