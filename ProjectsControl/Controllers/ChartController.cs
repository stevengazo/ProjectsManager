using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsControl.Models;
using System.Linq;
using System.Collections.Generic;
namespace ProjectsControl.Controllers
{
    public class ChartController : Controller
    {
        private readonly DBProjectContext _context;
        // GET: ChartController

        public ChartController(DBProjectContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        [Authorize(Roles = "Admin,Editor,Lector")]
        public ActionResult Projects()
        {
           /* Dictionary<int,int> ProjectsByYear = new Dictionary<int,int>();
            int[] years = (from project in _context.Projects
                           select project.BeginDate.Year).Distinct().ToArray();
            for (int i = 0; i < years.Length; i++)
            {
                var quantity = (from project in _context.Projects
                                where project.BeginDate.Year == years[i]
                                select project
                                ).Count();
                ProjectsByYear.Add(years[i], quantity);
            }
            ViewBag.ProjectsByYear = ProjectsByYear;*/
            return View();
        }



    }
}
