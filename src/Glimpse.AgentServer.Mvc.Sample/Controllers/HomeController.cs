using Microsoft.AspNet.Mvc;

namespace Glimpse.AgentServer.AspNet.Sample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
