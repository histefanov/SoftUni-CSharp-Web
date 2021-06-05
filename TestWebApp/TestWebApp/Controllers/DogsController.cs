using Microsoft.AspNetCore.Mvc;

namespace TestWebApp.Controllers
{
    public class DogsController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
