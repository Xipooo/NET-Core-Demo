using Microsoft.AspNetCore.Mvc;

namespace WozUCoreDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public string Index() {
            return "Home";
        }

        [HttpGet("{value}")]
        public string Index(string value){
            return value;
        }

        [HttpPost]
        public string Post([FromBody]string value){
            return "Posted Value:" + value;
        }
    }
}