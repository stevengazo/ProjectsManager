using Microsoft.AspNetCore.Mvc;

namespace ProjectsControl.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
