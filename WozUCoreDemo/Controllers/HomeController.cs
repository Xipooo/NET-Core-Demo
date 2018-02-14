using Microsoft.AspNetCore.Mvc;

namespace WozUCoreDemo.Controllers
{

// Use Route Attribute with placeholders to define endpoint
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public string Index() {
            return "Home";
        }
    }
}