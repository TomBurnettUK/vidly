using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            this.context.Dispose();
        }

        // GET /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return this.context.Customers
                .Include("MembershipType")
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
        }

        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = this.context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            this.context.Customers.Add(customer);
            this.context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri($"{Request.RequestUri}/{customer.Id}"), customerDto);
        }

        // PUT /api/customer/1
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerToEdit = this.context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerToEdit == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(customerDto, customerToEdit);

            this.context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerToDelete = this.context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerToDelete == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            this.context.Customers.Remove(customerToDelete);
            this.context.SaveChanges();
        }
    }
}
