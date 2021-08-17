using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectsControl.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ProjectsControl.Models;
namespace ProjectsControl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DBProjectContext _context;

        public HomeController(DBProjectContext context)
        {
            _context = context;
        }
      /*  public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Index()
        {
            ViewBag.Projects = (from proj in _context.Projects select proj).Where(P => P.IsOver != true).Include(C=>C.Customer).Include(S=>S.Saleman);
            ViewBag.NewBills = _context.Bill.FromSqlInterpolated($@"
                                                                SELECT TOP 10 * FROM Bill
                                                                Order by DateOfCreation DESC
                                                                ").ToList();
            ViewBag.NewExtras = (from extra in _context.ExtraHours select extra).Where(P => P.IsPaid != true).Include(W => W.Week).Include(E => E.Employee).ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
