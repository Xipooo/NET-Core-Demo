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

        // Convert return object to IActionResult and use base functions to return appropriate response
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return base.Ok(customers);
        }

        // Can use Json function to return an object
        // Update function to check if customer was found
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = customers.FirstOrDefault(cust => cust.Id == id);
            if (customer != null)
            {
                return base.Json(customer);
            }
            return base.NotFound();
        }

        // Update function to return proper result
        [HttpPost]
        public IActionResult AddCustomer([FromBody]Customer newCustomer)
        {
            // Use a try catch in case there is a problem adding the object to storage
            if (newCustomer != null)
            {
                try
                {
                    customers.Add(newCustomer);
                    return base.Created($"/{newCustomer.Id}", newCustomer);
                }
                catch (System.Exception ex)
                {
                    // Can specify status code to return and a message in the body of the response
                    return base.StatusCode(500, ex.Message);
                }
            }
            return base.BadRequest();
        }

        // Update method for appropriate response
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody]Customer updatedCustomer)
        {
            var customer = customers.FirstOrDefault(cust => cust.Id == id);
            if (customer != null)
            {
                try
                {
                    customer.FirstName = updatedCustomer.FirstName;
                    customer.LastName = updatedCustomer.LastName;
                    customer.Email = updatedCustomer.Email;
                    // Request worked but no content to return (Ok, but without a body)
                    return base.NoContent();
                }
                catch (System.Exception ex)
                {
                    // Error is probably because of bad data
                    return base.BadRequest();
                }
            }
            return base.NotFound();
        }

        // Update to send appropriate response
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = customers.FirstOrDefault(cust => cust.Id == id);
            if (customer != null)
            {
                customers.Remove(customer);
                return base.NoContent();
            }
            return base.NotFound();
        }
    }
}