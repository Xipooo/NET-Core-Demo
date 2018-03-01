using Microsoft.AspNetCore.Mvc;

namespace WozUCoreDemo.Controllers
{
    // Add new MVC controller for managing Customer Views
    public class HomePageController : Controller
    {
        // Add action method that returns the view of the same name (index.cshtml)
        public IActionResult Index() {
            return View();
        }
    }
}