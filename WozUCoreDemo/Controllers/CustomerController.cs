using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WozUCoreDemo.Models;
namespace WozUCoreDemo.Controllers
{
    // Add new Customer Controller for more parameter demonstration
    // Route does not indicate actions will be on this controller.  
    // HTTP actions will be used instead to determine which method to call.
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        // Internal variable to hold a list of customers
        private List<Customer> customers;
        public CustomerController()
        {
            // Create the initial values for customers List
            customers = new List<Customer>{
                new Customer { Id = 1, FirstName = "Steve", LastName = "Bishop", Email = "steve.bishop@codercamps.com" },
                new Customer { Id = 2, FirstName = "Sandra", LastName = "Marine", Email = "sandy_mar19@home.net" }
            };
        }

        // GET action to return all customers
        [HttpGet]
        public List<Customer> GetAllCustomers()
        {
            return customers;
        }

        // GET action to return a single customer
        [HttpGet("{id}")]
        public Customer GetCustomer(int id)
        {
            var customer = customers.FirstOrDefault(cust => cust.Id == id);
            return customer;
        }
    }
}