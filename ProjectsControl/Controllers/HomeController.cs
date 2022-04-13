using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectsControl.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace ProjectsControl.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private  DBProjectContext _context;

        public HomeController(DBProjectContext context)
        {
            this._context = context;
        }
      /*  public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/
      [AllowAnonymous]
        public IActionResult Index()
        {
          /*  ViewBag.Projects = (from proj in _context.Projects select proj).Where(P => P.IsOver != true).Include(C=>C.Customer).Include(S=>S.Saleman);
            ViewBag.NewBills = _context.Bill.FromSqlInterpolated($@"
                                                                SELECT TOP 10 * FROM Bill
                                                                Order by DateOfCreation DESC
                                                                ").ToList();
            ViewBag.NewExtras = (from extra in _context.ExtraHours select extra).Where(P => P.IsPaid != true).Include(W => W.Week).Include(E => E.Employee).ToList();
          */
            return View();
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
