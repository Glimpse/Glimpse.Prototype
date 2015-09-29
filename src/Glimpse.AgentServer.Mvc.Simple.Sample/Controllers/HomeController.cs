using Microsoft.AspNet.Mvc;

namespace Glimpse.AgentServer.Mvc.Simple.Sample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
