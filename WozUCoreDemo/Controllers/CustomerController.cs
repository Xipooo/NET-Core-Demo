using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WozUCoreDemo.Models;

namespace WozUCoreDemo.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private List<Customer> customers;
        public CustomerController()
        {
            customers = new List<Customer>{
                new Customer { Id = 1, FirstName = "Steve", LastName = "Bishop", Email = "steve.bishop@codercamps.com" },
                new Customer { Id = 2, FirstName = "Sandra", LastName = "Marine", Email = "sandy_mar19@home.net" }
            };
        }

        [HttpGet]
        public List<Customer> GetAllCustomers()
        {
            return customers;
        }

        [HttpGet("{id}")]
        public Customer GetCustomer(int id)
        {
            var customer = customers.FirstOrDefault(cust => cust.Id == id);
            return customer;
        }

        // Use POST action to create new Customer
        [HttpPost]
        public Customer AddCustomer([FromBody]Customer newCustomer)
        {
            if (newCustomer != null)
            {
                customers.Add(newCustomer);
            }
            return newCustomer;
        }

        // Use PUT action to update a customer
        [HttpPut("{id}")]
        public Customer UpdateCustomer(int id, [FromBody]Customer updatedCustomer)
        {
            var customer = customers.FirstOrDefault(cust => cust.Id == id);
            if (customer != null)
            {
                customer.FirstName = updatedCustomer.FirstName;
                customer.LastName = updatedCustomer.LastName;
                customer.Email = updatedCustomer.Email;
                return customer;
            }
            return null;
        }

        // Use DELETE action to delete customer
        [HttpDelete("{id}")]
        public Customer DeleteCustomer(int id)
        {
            var customer = customers.FirstOrDefault(cust => cust.Id == id);
            if (customer != null)
            {
                customers.Remove(customer);
                return customer;
            }
            return null;
        }
    }
}