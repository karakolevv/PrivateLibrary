using Microsoft.AspNetCore.Mvc;

namespace PrivateLibrary.Controllers
{
    public class AdministratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
