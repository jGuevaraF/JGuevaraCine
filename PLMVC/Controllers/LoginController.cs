using Microsoft.AspNetCore.Mvc;

namespace PLMVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
