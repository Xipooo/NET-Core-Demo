using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WozUCoreDemo.Models;

namespace WozUCoreDemo.Controllers
{
    public class HomePageController : Controller
    {
        // Use dependency injection to get the database context
        private readonly WozUContext _context;
        public HomePageController(WozUContext context)
        {
            _context = context;
        }
        public IActionResult Index() {

            // Get a list of all the customers in the system.
            List<Customer> customers = _context.Customers.ToList();

            // Pass customers to the view
            return View(customers);
        }

        [HttpGet]
        public IActionResult Edit(int id){
            Customer customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer editedCustomer){
            Customer originalCustomer = _context.Customers.FirstOrDefault(c => c.Id == editedCustomer.Id);
            originalCustomer.FirstName = editedCustomer.FirstName;
            originalCustomer.LastName = editedCustomer.LastName;
            originalCustomer.Email = editedCustomer.Email;
            _context.Customers.Update(originalCustomer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}