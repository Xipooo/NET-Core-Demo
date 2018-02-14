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

        // Add get that receives value from URL
        [HttpGet("{value}")]
        public string Index(string value){
            return value;
        }
        
        // Add post that receives value from body of request
        [HttpPost]
        public string Post([FromBody]string value){
            return "Posted Value:" + value;
        }
    }
}