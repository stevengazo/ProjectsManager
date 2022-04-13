using Microsoft.AspNetCore.Mvc;

namespace ProjectsControl.Areas.Admin.Controllers
{
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
