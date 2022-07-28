using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChartJSCore.Helpers;
using ChartJSCore.Models;
using ChartJSCore.Models.ChartJSCore.Models;
using ChartJSCore.Models.Polar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjectsControl.Models;


namespace ProjectsControl.Controllers
{
    public class StadisticController : Controller
    {
        private readonly DBProjectContext _context;

        public StadisticController(DBProjectContext context)
        {
            _context = context;
        }


        // GET: StadisticController
        public ActionResult Index()
        {
            return View();
        }


        public IActionResult Ventas()
        {
            return View();
        }

    
    }
}
