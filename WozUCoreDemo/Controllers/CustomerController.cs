using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WozUCoreDemo.Models;

namespace WozUCoreDemo.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        // Dependency injection to get the Database Context
        private WozUContext _context;
        public CustomerController(WozUContext context)
        {
            _context = context;
        }

        // Update to use the Database Context
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _context.Customers.ToList();
            return base.Ok(customers);
        }

        // Use the Database Context
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(cust => cust.Id == id);
            if (customer != null)
            {
                return base.Json(customer);
            }
            return base.NotFound();
        }

        // Use the Database Context
        [HttpPost]
        public IActionResult AddCustomer([FromBody]Customer newCustomer)
        {
            if (newCustomer != null)
            {
                try
                {
                    _context.Customers.Add(newCustomer);
                    _context.SaveChanges();
                    return base.Created($"/{newCustomer.Id}", newCustomer);
                }
                catch (System.Exception ex)
                {
                    return base.StatusCode(500, ex.Message);
                }
            }
            return base.BadRequest();
        }

        // Use the Database Context
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody]Customer updatedCustomer)
        {
            var customer = _context.Customers.FirstOrDefault(cust => cust.Id == id);
            if (customer != null)
            {
                try
                {
                    customer.FirstName = updatedCustomer.FirstName;
                    customer.LastName = updatedCustomer.LastName;
                    customer.Email = updatedCustomer.Email;
                    _context.Update(customer);
                    _context.SaveChanges();
                    return base.NoContent();
                }
                catch (System.Exception ex)
                {
                    return base.BadRequest(ex.Message);
                }
            }
            return base.NotFound();
        }

        // Use the Database Context
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(cust => cust.Id == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return base.NoContent();
            }
            return base.NotFound();
        }
    }
}