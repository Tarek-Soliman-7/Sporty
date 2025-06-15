using Microsoft.AspNetCore.Mvc;

namespace Sporty.Controllers
{
    public class MembershipTypes : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
